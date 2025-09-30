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
        RuleFor(x => x.MedicationName)
            .NotEmpty();

        RuleFor(x => x.Dosage)
            .NotEmpty();

        RuleFor(x => x.Duration)
            .NotEmpty();

        RuleFor(x => x.Notes)
            .NotEmpty();
    }
}
