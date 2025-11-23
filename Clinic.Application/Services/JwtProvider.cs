using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Clinic.Application.Services;
internal class JwtProvider(IOptions<JwtOptions> jwtOptions,ILogger<JwtProvider> logger) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
    {
        Claim[] claims = [
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.GivenName, user.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(nameof(roles), JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
            new(nameof(permissions), JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray)
        ];
        logger.LogInformation("Key {key}", _jwtOptions.Key);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
            signingCredentials: singingCredentials
        );

        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _jwtOptions.ExpiryInMinutes * 60);
    }


    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch
        {

            return null;
        }
    }
}
