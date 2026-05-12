namespace Clinic.Application.Interfaces.Services;

public interface IUserRsaKeyService
{
    Task<string> GetOrCreatePublicKeyPemAsync(string userId, CancellationToken cancellationToken = default);
    Task<string> GetOrCreatePrivateKeyPemAsync(string userId, CancellationToken cancellationToken = default);
}
