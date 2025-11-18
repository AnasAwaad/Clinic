using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Mapping.Resolvers;
public class PatientImageResolver(IHttpContextAccessor httpContext) : IValueResolver<Patient, object, string>
{

    public string Resolve(Patient source, object destination, string destMember, ResolutionContext context)
    {
        if(!string.IsNullOrEmpty(source.ImageUrl))
        {
            var request = httpContext.HttpContext!.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";
            return $"{baseUrl}{source.ImageUrl}";
        }

        return string.Empty;
    }
}
