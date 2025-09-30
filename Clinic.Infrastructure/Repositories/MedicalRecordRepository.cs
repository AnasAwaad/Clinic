using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
internal class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(DbContext context) : base(context)
    {
    }

    public async Task<bool> MedicalRecordIsExists(int recordId)
    {
        return await _context.Set<MedicalRecord>()
            .AnyAsync(x => x.Id == recordId);
    }
}
