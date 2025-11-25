using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patient;
using Clinic.Domain.Helpers;

namespace Clinic.Application.Interfaces.Services;
public interface IPatientService
{
    Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatientsAsync();
    Task<Result<PaginatedList<PatientResposne>>> GetAllAsync(RequestFilters request);
    Task<Result<PatientResposne>> GetByIdAsync(string id);
    Task<Result<PatientResposne>> CreateAsync(PatientRequest request);
    Task<Result> UpdateAsync(string id, UpdatePatientRequest request);
    Task<Result> DeleteAsync(string id);
    Result DeleteMany(List<string> ids);
    Task<Result<PatientProfileResponse>> GetPatientProfileDetailsAsync(string id);
}
