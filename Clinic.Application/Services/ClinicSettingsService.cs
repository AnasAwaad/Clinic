using AutoMapper;
using Clinic.Application.DTOs.Clinic;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
public class ClinicSettingsService(IUnitOfWork unitOfWork,IMapper mapper,IFileService fileService) : IClinicSettingsService
{
    public async Task<ClinicSettingsResponse> GetAsync()
    {
        var settings = await unitOfWork.ClinicSettings.GetByIdAsync(1);

        if(settings is null)
        {
            settings = new ClinicSettings
            {
                Id = 1
            };
            await unitOfWork.ClinicSettings.AddAsync(settings);
            await unitOfWork.SaveAsync();
        }
        return mapper.Map<ClinicSettingsResponse>(settings);
    }

    public async Task UpdateAsync(ClinicSettingsRequest request)
    {
        var settings = await unitOfWork.ClinicSettings.GetByIdAsync(1);


        if(request.Logo is not null)
        {
            settings!.LogoUrl = await fileService.UploadFileAsync(request.Logo, "clinic");
        }

        mapper.Map(request, settings);

        await unitOfWork.SaveAsync();
    }
}
