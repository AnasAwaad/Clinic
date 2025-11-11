using Clinic.Application.DTOs.Patient;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Infrastructure.Repositories;
internal class PatientRespository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRespository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PatientActiveResponse>> GetAllActiveAsync()
    {
        var now = DateOnly.FromDateTime(DateTime.Now);
        return await _context.Set<Patient>()
            .Where(p => !p.IsDisabled && !p.Appointments.Any(a=>a.Date > now && a.Status=="Booked"))
            .Select(p => new PatientActiveResponse
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                ImageUrl = p.ImageUrl,
                PhoneNumber = p.PhoneNumber
            }).ToListAsync();
    }

    public IQueryable<Patient> GetAllWithDetailsQueryable()
    {
        return  _context.Set<Patient>()
            .AsNoTracking()
            .AsQueryable();
    }

    public Task<Patient?> GetByUserIdAsync(string userId)
    {
        return _context.Set<Patient>().SingleOrDefaultAsync(u => u.Id == userId);
    }
}
