using Clinic.Application.DTOs.Result;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
public class ResultService(IUnitOfWork unitOfWork) : IResultService
{

    public async Task<IEnumerable<PatientsPerDayResponse>> GetPatientsPerDayAsync()
    {
        return await unitOfWork.Patients.GetPatientsPerDaysAsync();
    }


    public async Task<IEnumerable<AppointmentsPerDayResponse>> GetAppointmentsPerDayAsync()
    {
        return await unitOfWork.Appointments.GetAppointmentsPerDaysAsync();
    }
}
