using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

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
            .Where(p => !p.IsDisabled && !p.IsDeleted)
            .Select(p => new PatientActiveResponse
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                ImageUrl = p.ImageUrl,
                PhoneNumber = p.PhoneNumber
            }).ToListAsync();
    }

    public IQueryable<Patient> GetAllWithDetailsQueryable(RequestFilters filters)
    {
        var query = _context
            .Set<Patient>()
            .AsNoTracking()
            .Where(p => !p.IsDeleted);

        if (!string.IsNullOrEmpty(filters.SearchValue))
        {
            var searchValue = filters.SearchValue.Trim();
            query = query.Where(p =>
                p.FirstName.Contains(searchValue) ||
                p.LastName.Contains(searchValue) ||
                p.Email.Contains(searchValue) ||
                p.PhoneNumber.Contains(searchValue));
        }

        if (!string.IsNullOrEmpty(filters.SortColumn))
        {
            query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
        }

        return query
            .Include(x=>x.Appointments).AsQueryable();
    }

    public Task<Patient?> GetByUserIdAsync(string userId)
    {
        return _context.Set<Patient>().SingleOrDefaultAsync(u => u.Id == userId);
    }
}
