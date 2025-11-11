using Clinic.Application.DTOs.Patient;
using Clinic.Domain.Helpers;

namespace Clinic.Application.Interfaces.Services;
public interface IPatientService
{
    Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatients();
    Task<Result<PaginatedList<PatientResposne>>> GetAll(int pageNumber, int pageSize);
    Task<Result<PatientResposne>> GetById(string id);
    Task<Result<PatientResposne>> CreateAsync(PatientRequest request);
    Task<Result> Update(int id,PatientRequest request);
    Task<Result> Delete(int id);
}
