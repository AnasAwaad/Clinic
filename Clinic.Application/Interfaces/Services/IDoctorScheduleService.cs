using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IDoctorScheduleService
{
    Task<Result<DoctorScheduleResponse>> CreateAsync(int doctorId, string day, DoctorScheduleRequest request);
}
