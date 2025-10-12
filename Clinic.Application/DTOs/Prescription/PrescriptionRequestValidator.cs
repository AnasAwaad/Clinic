using Clinic.Application.DTOs.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
internal class PrescriptionRequestValidator : AbstractValidator<PrescriptionRequest>
{
    public PrescriptionRequestValidator()
    {
        RuleFor(x => x.PatientId)
            .NotEmpty();

        RuleFor(x => x.Date)
            .NotEmpty();


        RuleFor(x => x.Age)
            .NotEmpty();


        RuleFor(x => x.Diagnosis)
            .NotEmpty();


        RuleFor(x => x.NextVisit)
            .NotEmpty();

        RuleFor(x => x.Notes)
            .NotEmpty();

        RuleFor(x => x.Items)
            .NotEmpty();

        RuleFor(x => x.Items)
            .Must(x => x.Count > 0)
            .When(x => x.Items is not null);

    }
}
