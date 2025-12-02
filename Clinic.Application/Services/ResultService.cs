using Clinic.Application.DTOs.Result;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Clinic.Application.Services;
public class ResultService(IUnitOfWork unitOfWork) : IResultService
{

    public async Task<IEnumerable<PatientsPerDayResponse>> GetPatientsPerDayAsync()
    {
        return await unitOfWork.Patients.GetPatientsPerDaysAsync();
    }


    public async Task<IEnumerable<AppointmentsPerDayResponse>> GetAppointmentsPerDayAsync()
    {
        return await unitOfWork.Appointments.GetAppointmentsPerDaysAsync();
    }

    public async Task<AppointmentStatusResponse> GetStatusAsync(string period, DateOnly? start = null, DateOnly? end = null, int? year = null, CancellationToken cancellationToken = default)
    {
        period = (period ?? "monthly").Trim().ToLowerInvariant();

        IQueryable<Appointment> query = unitOfWork.Appointments.GetAllQueryable();

        if (start.HasValue) query = query.Where(a => a.Date >= start.Value);
        if (end.HasValue) query = query.Where(a => a.Date <= end.Value);

        if (year.HasValue)
        {
            var y = year.Value;
            query = query.Where(a => a.Date.Year == y);
        }


        var totals = await query
            .GroupBy(a => 1)
            .Select(g => new
            {
                All = g.Count(),
                Completed = g.Count(a => a.Status == AppointmentStatus.Completed),
                Booked = g.Count(a => a.Status == AppointmentStatus.Booked),
                Cancelled = g.Count(a => a.Status == AppointmentStatus.Cancelled)
            })
            .FirstOrDefaultAsync(cancellationToken);

        // Build segments depending on period
        List<TimeSegmentStatsResponse> segments = period switch
        {
            "weekly" => await BuildWeeklySegmentsAsync(query, start, end, cancellationToken),
            "monthly" => await BuildMonthlySegmentsAsync(query, year, start, end, cancellationToken),
            "yearly" => await BuildYearlySegmentsAsync(query, start, end, cancellationToken),
            _ => await BuildMonthlySegmentsAsync(query, year, start, end, cancellationToken)
        };

        var dto = new AppointmentStatusResponse
        {
            AllAppointmentsCount = totals?.All ?? 0,
            CompletedCount = totals?.Completed ?? 0,
            BookedCount = totals?.Booked ?? 0,
            CancelledCount = totals?.Cancelled ?? 0,
            SegmentStatus = segments
        };

        return dto;
    }

    private async Task<List<TimeSegmentStatsResponse>> BuildMonthlySegmentsAsync(IQueryable<Appointment> baseQuery, int? year, DateOnly? start, DateOnly? end, CancellationToken ct)
    {
        // If year not provided, use current year
        int targetYear = year ?? DateOnly.FromDateTime(DateTime.UtcNow).Year;

        // Ensure we limit to the targetYear
        var q = baseQuery.Where(a => a.Date.Year == targetYear);

        // Group by month number
        var grouped = await q
            .GroupBy(a => a.Date.Month)
            .Select(g => new
            {
                Month = g.Key,
                Completed = g.Count(a => a.Status == AppointmentStatus.Completed),
                Booked = g.Count(a => a.Status == AppointmentStatus.Booked),
                Cancelled = g.Count(a => a.Status == AppointmentStatus.Cancelled)
            })
            .ToListAsync(ct);

        // Build array of 12 months - fill missing months with zero
        var months = CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames; // Jan, Feb, ...
        var result = new List<TimeSegmentStatsResponse>();
        for (int m = 1; m <= 12; m++)
        {
            var g = grouped.FirstOrDefault(x => x.Month == m);
            result.Add(new TimeSegmentStatsResponse
            {
                Label = months[m - 1],
                Completed = g?.Completed ?? 0,
                Booked = g?.Booked ?? 0,
                Cancelled = g?.Cancelled ?? 0
            });
        }
        return result;
    }

    private async Task<List<TimeSegmentStatsResponse>> BuildWeeklySegmentsAsync(IQueryable<Appointment> baseQuery, DateOnly? start, DateOnly? end, CancellationToken ct)
    {
        // We need start and end to create a sequence of weeks. If not provided, default to last 4 full weeks.
        DateOnly utcNow = DateOnly.FromDateTime(DateTime.UtcNow);
        DateOnly endDate = (end ?? utcNow);
        DateOnly startDate = (start ?? endDate.AddDays(-28)); // previous ~4 weeks

        // Normalize to week start (Sunday) 
        DateOnly GetWeekStart(DateOnly d) => d.AddDays(-(int)d.DayOfWeek + (int)DayOfWeek.Sunday);

        var weekStarts = new List<DateOnly>();
        DateOnly cur = GetWeekStart(startDate);
        while (cur <= endDate)
        {
            weekStarts.Add(cur);
            cur = cur.AddDays(7);
        }


        var raw = await baseQuery
            .Where(a => a.Date >= startDate && a.Date <= endDate)
            .Select(a => new { a.Date, a.Status })
            .ToListAsync(ct);

        var groups = raw
            .GroupBy(x => GetWeekStart(x.Date))
            .OrderBy(g => g.Key)
            .Select(g => new TimeSegmentStatsResponse
            {
                Label = $"W {g.Key:MM-dd}", // example label: W 11-03
                Completed = g.Count(x => x.Status == AppointmentStatus.Completed),
                Booked = g.Count(x => x.Status == AppointmentStatus.Booked),
                Cancelled = g.Count(x => x.Status == AppointmentStatus.Cancelled)
            })
            .ToList();

        // ensure all weekStarts present (fill zeros)
        var final = new List<TimeSegmentStatsResponse>();
        foreach (var ws in weekStarts)
        {
            var existing = groups.FirstOrDefault(x => x.Label == $"W {ws:MM-dd}");
            if (existing != null) final.Add(existing);
            else final.Add(new TimeSegmentStatsResponse { Label = $"W {ws:MM-dd}", Completed = 0, Booked = 0, Cancelled = 0 });
        }
        return final;
    }

    private async Task<List<TimeSegmentStatsResponse>> BuildYearlySegmentsAsync(IQueryable<Appointment> baseQuery, DateOnly? start, DateOnly? end, CancellationToken ct)
    {
        // Group by year (e.g., last N years in range). If not limited, return last 5 years.
        DateOnly utcNow = DateOnly.FromDateTime(DateTime.UtcNow);
        int startYear = start?.Year ?? (utcNow.Year - 4);
        int endYear = end?.Year ?? utcNow.Year;

        var grouped = await baseQuery
            .Where(a => a.Date.Year >= startYear && a.Date.Year <= endYear)
            .GroupBy(a => a.Date.Year)
            .Select(g => new
            {
                Year = g.Key,
                Completed = g.Count(a => a.Status == AppointmentStatus.Completed),
                Booked = g.Count(x => x.Status == AppointmentStatus.Booked),
                Cancelled = g.Count(x => x.Status == AppointmentStatus.Cancelled)
            })
            .ToListAsync(ct);

        var result = new List<TimeSegmentStatsResponse>();
        for (int y = startYear; y <= endYear; y++)
        {
            var g = grouped.FirstOrDefault(x => x.Year == y);
            result.Add(new TimeSegmentStatsResponse
            {
                Label = y.ToString(),
                Completed = g?.Completed ?? 0,
                Booked = g?.Booked ?? 0,
                Cancelled = g?.Cancelled ?? 0
            });
        }
        return result;
    }
}
