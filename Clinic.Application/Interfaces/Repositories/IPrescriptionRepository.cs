using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IPrescriptionRepository : IGenericRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetAllByMedicalRecordAsync(int recordId);
    Task<Prescription?>GetByMedicalRecordAsync(int recordId, int id);
}
