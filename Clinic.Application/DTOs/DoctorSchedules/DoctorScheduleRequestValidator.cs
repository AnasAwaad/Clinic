using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.DoctorSchedules;
public class DoctorScheduleRequestValidator : AbstractValidator<DoctorScheduleRequest>
{
    public DoctorScheduleRequestValidator()
    {
        RuleFor(x => x.EndTime)
            .NotEmpty()
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
            .WithMessage("End time must be in HH:mm format");

        RuleFor(x=>x.StartTime)
            .NotEmpty()
            .Matches(@"^(?:[01]\d|2[0-3]):[0-5]\d$")
            .WithMessage("Start time must be in HH:mm format");

        RuleFor(x=>x)
            .Must(x=> TimeOnly.Parse(x.EndTime) > TimeOnly.Parse(x.StartTime))
            .WithMessage("End time must be greater than start time");

    }
}
