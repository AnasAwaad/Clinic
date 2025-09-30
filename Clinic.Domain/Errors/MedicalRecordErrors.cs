using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Errors;
public class MedicalRecordErrors
{
    public static readonly Error MedicalRecordNotFound = new("MedicalRecord.NotFound", "No medical record was found with the given Id", StatusCodes.Status404NotFound);
}
