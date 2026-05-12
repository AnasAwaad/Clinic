using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Repositories;
public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(DbContext context) : base(context)
    {
    }

    public async Task<Doctor?> GetDoctor()
    {
        return await _context.Set<Doctor>()
            .FirstOrDefaultAsync();
    }
}
