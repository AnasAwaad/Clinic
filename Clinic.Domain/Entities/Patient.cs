using Clinic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;
public class Patient : ApplicationUser
{
    public DateOnly? DateOfBirth { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = default!;

}
