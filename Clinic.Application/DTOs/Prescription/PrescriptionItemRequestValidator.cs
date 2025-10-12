using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Prescription;
public class PrescriptionItemRequestValidator : AbstractValidator<PrescriptionItemRequest>  
{
    public PrescriptionItemRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Dosage)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Frequency)
            .GreaterThan(0);

        RuleFor(x => x.Days)
            .GreaterThan(0);

        RuleFor(x => x.Instructions)
            .NotEmpty();
    }
}
