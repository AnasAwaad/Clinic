using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Result;
public class AppointmentsPerDayResponse
{
    public DateOnly Date { get; set; }
    public int Count { get; set; }
}
