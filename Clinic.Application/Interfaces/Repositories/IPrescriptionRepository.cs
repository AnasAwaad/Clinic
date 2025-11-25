using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Repositories;
public interface IPrescriptionRepository : IGenericRepository<Prescription>
{
    Task<Prescription?> GetByIdWithItemsAsync(int id);
    IQueryable<Prescription> GetAllWithItemsQueryable(string? searchValue);
    IQueryable<Prescription> GetAllByPatientIdWithItemsQueryable(string patientId);
}
