using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Clinic.Infrastructure.Services;

public sealed class UserRsaKeyService(
    UserManager<ApplicationUser> userManager,
    IDataProtectionProvider dataProtectionProvider) : IUserRsaKeyService
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> Locks = new();
    private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector("Clinic.RsaPrivateKeyPem.v1");

    public async Task<string> GetOrCreatePublicKeyPemAsync(string userId, CancellationToken cancellationToken = default)
    {
        var (publicPem, _) = await GetOrCreateKeyPairAsync(userId, cancellationToken);
        return publicPem;
    }

    public async Task<string> GetOrCreatePrivateKeyPemAsync(string userId, CancellationToken cancellationToken = default)
    {
        var (_, privatePem) = await GetOrCreateKeyPairAsync(userId, cancellationToken);
        return privatePem;
    }

    private async Task<(string PublicPem, string PrivatePem)> GetOrCreateKeyPairAsync(string userId, CancellationToken cancellationToken)
    {
        var gate = Locks.GetOrAdd(userId, static _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(cancellationToken);
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                throw new InvalidOperationException("User not found");

            if (!string.IsNullOrWhiteSpace(user.RsaPublicKeyPem) && !string.IsNullOrWhiteSpace(user.ProtectedRsaPrivateKeyPem))
            {
                var privatePemExisting = _protector.Unprotect(user.ProtectedRsaPrivateKeyPem);
                return (user.RsaPublicKeyPem, privatePemExisting);
            }

            using var rsa = RSA.Create(2048);
            var publicPem = rsa.ExportSubjectPublicKeyInfoPem();
            var privatePem = rsa.ExportPkcs8PrivateKeyPem();

            user.RsaPublicKeyPem = publicPem;
            user.ProtectedRsaPrivateKeyPem = _protector.Protect(privatePem);

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var first = updateResult.Errors.FirstOrDefault();
                throw new InvalidOperationException(first?.Description ?? "Failed to persist RSA keys");
            }

            return (publicPem, privatePem);
        }
        finally
        {
            gate.Release();
        }
    }
}
