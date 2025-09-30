using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public static class PrescriptionErrors
{
    public static readonly Error PrescriptionNotFound = new("Prescriptions.NotFound", "Prescription with the given id not found", StatusCodes.Status400BadRequest);
}