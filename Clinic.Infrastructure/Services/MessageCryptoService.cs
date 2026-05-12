using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Clinic.Infrastructure.Services;

public sealed class MessageCryptoService(IUserRsaKeyService userRsaKeyService) : IMessageCryptoService
{
    public async Task<EncryptedMessagePayload> EncryptAsync(string plaintext, string receiverUserId, CancellationToken cancellationToken = default)
    {
        if (plaintext is null)
            throw new ArgumentNullException(nameof(plaintext));

        var receiverPublicKey = await userRsaKeyService.GetOrCreatePublicKeyPemAsync(receiverUserId, cancellationToken);

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.GenerateKey();
        aes.GenerateIV();

        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        byte[] encryptedMessage;

        using (var ms = new MemoryStream())
        using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
        {
            cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            cryptoStream.FlushFinalBlock();
            encryptedMessage = ms.ToArray();
        }

        using var rsa = RSA.Create();
        rsa.ImportFromPem(receiverPublicKey);

        var encryptedAesKey = rsa.Encrypt(aes.Key, RSAEncryptionPadding.OaepSHA256);

        return new EncryptedMessagePayload(encryptedMessage, encryptedAesKey, aes.IV);
    }

    public async Task<string> DecryptAsync(Message message, CancellationToken cancellationToken = default)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        var receiverPrivatePem = await userRsaKeyService.GetOrCreatePrivateKeyPemAsync(message.ReceiverId, cancellationToken);

        using var rsa = RSA.Create();
        rsa.ImportFromPem(receiverPrivatePem);

        var aesKey = rsa.Decrypt(message.EncryptedAesKey, RSAEncryptionPadding.OaepSHA256);

        using var aes = Aes.Create();
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream(message.EncryptedMessage);
        using var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(aesKey, message.Iv), CryptoStreamMode.Read);
        using var reader = new StreamReader(cryptoStream, Encoding.UTF8);

        return await reader.ReadToEndAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<string>> DecryptManyAsync(IReadOnlyList<Message> messages, CancellationToken cancellationToken = default)
    {
        if (messages is null)
            throw new ArgumentNullException(nameof(messages));

        var receiverIds = messages.Select(m => m.ReceiverId).Distinct().ToArray();
        var privateKeysByReceiver = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var receiverId in receiverIds)
        {
            privateKeysByReceiver[receiverId] = await userRsaKeyService.GetOrCreatePrivateKeyPemAsync(receiverId, cancellationToken);
        }

        var result = new string[messages.Count];

        for (var i = 0; i < messages.Count; i++)
        {
            var message = messages[i];
            var receiverPrivatePem = privateKeysByReceiver[message.ReceiverId];

            using var rsa = RSA.Create();
            rsa.ImportFromPem(receiverPrivatePem);
            var aesKey = rsa.Decrypt(message.EncryptedAesKey, RSAEncryptionPadding.OaepSHA256);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var ms = new MemoryStream(message.EncryptedMessage);
            using var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(aesKey, message.Iv), CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream, Encoding.UTF8);
            result[i] = await reader.ReadToEndAsync(cancellationToken);
        }

        return result;
    }
}
