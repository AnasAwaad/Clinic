using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.DTOs.Result;
using Clinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<Patient?> GetByUserIdAsync(string userId);
    Task<IEnumerable<PatientActiveResponse>> GetAllActiveAsync();
    IQueryable<Patient> GetAllWithDetailsQueryable(RequestFilters filters);
    Task<IEnumerable<PatientsPerDayResponse>> GetPatientsPerDaysAsync();
    void DeleteMany(List<string> idList);
}
