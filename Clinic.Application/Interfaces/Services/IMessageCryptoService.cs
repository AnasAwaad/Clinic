using Clinic.Domain.Entities;

namespace Clinic.Application.Interfaces.Services;

public sealed record EncryptedMessagePayload(byte[] EncryptedMessage, byte[] EncryptedAesKey, byte[] Iv);

public interface IMessageCryptoService
{

    Task<EncryptedMessagePayload> EncryptAsync(string plaintext, string receiverUserId, CancellationToken cancellationToken = default);
    Task<string> DecryptAsync(Message message, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> DecryptManyAsync(IReadOnlyList<Message> messages, CancellationToken cancellationToken = default);
}
