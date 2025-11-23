using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Clinic.Application.Interfaces.Services.IGoogleAuthService;

namespace Clinic.Infrastructure.Services;
internal class GoogleAuthService : IGoogleAuthService
{
    private readonly string[] _audience;

    public GoogleAuthService(string clientId)
    {
        _audience = new[] { clientId };
    }

    public async Task<IGoogleAuthService.GoogleUserDto?> ValidateIdTokenAsync(string idToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = _audience
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            
            return new GoogleUserDto(Subject: payload.Subject,Email: payload.Email,FullName: payload.GivenName,Picture: payload.Picture);
        }
        catch (Exception ex)
        {
            //invalid token
            return null;
        }
    }
}
