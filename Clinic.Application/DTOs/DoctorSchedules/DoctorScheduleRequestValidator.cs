using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.DoctorSchedules;
public class TimeSlotRequestValidator : AbstractValidator<TimeSlotRequest>
{
    public TimeSlotRequestValidator()
    {
        RuleFor(x => x.EndTime)
            .NotEmpty()
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
            .WithMessage("End time must be in HH:mm format");

        RuleFor(x=>x.StartTime)
            .NotEmpty()
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
            .WithMessage("Start time must be in HH:mm format");

        RuleFor(x => x)
            .Must(x =>
            {
                if (!TimeOnly.TryParse(x.StartTime, out var start)) return false;
                if (!TimeOnly.TryParse(x.EndTime, out var end)) return false;
                var duration = end - start;
                return duration.TotalMinutes >= 15 && duration.TotalMinutes <= 240;
            })
            .WithName("TimeSlotDuration")
            .WithMessage("StartTime must be greater than end time and Time slot must be between 15 minutes and 4 hours.");

    }
}
