using Clinic.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Clinic.Domain.Errors;
public static class DoctorErrors
{
    public static readonly Error DoctorNotFound = new("Doctor.NotFound", "No doctor was found with the given Id", StatusCodes.Status404NotFound);
}
