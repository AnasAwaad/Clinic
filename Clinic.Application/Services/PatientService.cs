using Clinic.Application.DTOs.Patient;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;

namespace Clinic.Application.Services;
public class PatientService(IUnitOfWork unitOfWork) : IPatientService
{
    public async Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatients()
    {
        return Result.Success(await unitOfWork.Patients.GetAllActiveAsync());
    }
}
