using Clinic.Application.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IPatientService
{
    Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatients();
}
