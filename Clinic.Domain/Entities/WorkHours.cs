using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

[Owned]
public class WorkHours
{
    public DayHours? Monday { get; set; }
    public DayHours? Tuesday { get; set; }
    public DayHours? Wednesday { get; set; }
    public DayHours? Thursday { get; set; }
    public DayHours? Friday { get; set; }
    public DayHours? Saturday { get; set; }
    public DayHours? Sunday { get; set; }

}
