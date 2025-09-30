using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Prescription>> GetAllByMedicalRecordAsync(int recordId)
    {
        return await _context.Set<Prescription>()
            .Where(x => x.MedicalRecordId == recordId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Prescription?> GetByMedicalRecordAsync(int recordId,int id)
    {
        return await _context.Set<Prescription>()
            .Where(x => x.MedicalRecordId == recordId && x.Id == id)
            .FirstOrDefaultAsync();
    }
}
