using Clinic.Application.DTOs.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IClinicSettingsService
{
    Task<ClinicSettingsResponse> GetAsync();
    Task UpdateAsync(ClinicSettingsRequest request);
}
