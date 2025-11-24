using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities;

[Owned]
public class DayHours
{
    public string? Open { get; set; }
    public string? Close { get; set; }
    public bool? IsClose { get; set; }
}
