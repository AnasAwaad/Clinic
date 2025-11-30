using Clinic.Application.DTOs.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IResultService
{
    Task<IEnumerable<PatientsPerDayResponse>> GetPatientsPerDayAsync();
    Task<IEnumerable<AppointmentsPerDayResponse>> GetAppointmentsPerDayAsync();
}
