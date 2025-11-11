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

    public Task<Prescription?> GetByIdWithItemsAsync(int id)
    {
        return _context.Set<Prescription>()
            .Include(p => p.Items)
            .Include(p=>p.Patient)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Prescription> GetAllWithItemsQueryable()
    {
        return _context.Set<Prescription>()
            .Include(p => p.Items)
            .AsQueryable();
    }
}
