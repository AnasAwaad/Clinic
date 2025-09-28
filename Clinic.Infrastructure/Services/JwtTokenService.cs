using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
    }

    public string GenerateToken(ApplicationUser appUser, string role)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.GivenName, appUser.FirstName),
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(ClaimTypes.Role ,role),
            new Claim("UserName", appUser.UserName),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

        var tokenDesciptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _configuration["Token:Issuer"],
            IssuedAt = DateTime.Now,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDesciptor);

        return tokenHandler.WriteToken(token);
    }
}
