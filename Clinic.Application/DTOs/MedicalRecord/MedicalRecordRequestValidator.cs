using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.MedicalRecord;
public class MedicalRecordRequestValidator : AbstractValidator<MedicalRecordRequest>
{
    public MedicalRecordRequestValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty();

        RuleFor(x => x.Diagnosis)
            .NotEmpty();

        RuleFor(x => x.Treatment)
            .NotEmpty();
    }
}
