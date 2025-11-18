using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class PatientErrors
{
    public static readonly Error PatientNotFound = new("Patient.PatientNotFound", "Patient with the given id not found", StatusCodes.Status400BadRequest);
    public static readonly Error PatientAlreadyDeleted = new("Patient.PatientAlreadyDeleted", "Patient with the given id is already deleted", StatusCodes.Status400BadRequest);
    public static readonly Error NoIdsProvided = new("Patient.NoIdsProvided", "No ids is provided", StatusCodes.Status400BadRequest);
}