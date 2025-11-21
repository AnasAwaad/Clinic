using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.Common;
using Clinic.Application.DTOs.Patient;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Application.Services;
public class PatientService(UserManager<ApplicationUser> userManager,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IFileService fileService) : IPatientService
{
    public async Task<Result<PatientResposne>> CreateAsync(PatientRequest request)
    {
        var emailIsExists = await userManager.Users.AnyAsync(u=>u.Email == request.Email);  

        if (emailIsExists)
            return Result.Failure<PatientResposne>(UserErrors.DuplicatedEmail);

        var user = mapper.Map<Patient>(request);

        if(request.Image is not null)
        {
            var imageUrl = await fileService.UploadFileAsync(request.Image, "uploads/profiles");
            user.ImageUrl = imageUrl;
        }
        else
            user.ImageUrl = "/uploads/uploads/profiles/avatar.png";

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<PatientResposne>(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest));
        }

        await userManager.AddToRoleAsync(user, AppRoles.Patient);

        await unitOfWork.SaveAsync();


        return Result.Success(mapper.Map<PatientResposne>(user));
    }

    public async Task<Result> UpdateAsync(string id, UpdatePatientRequest request)
    {
        var emailExists = await userManager.Users.AnyAsync(u => u.Email == request.Email && u.Id != id);

        if (emailExists)
            return Result.Failure(UserErrors.DuplicatedEmail);

        var userNameExists = await userManager.Users.AnyAsync(u => u.UserName == request.UserName && u.Id != id);

        if (userNameExists)
            return Result.Failure(UserErrors.DuplicatedUsername);

        var user = await userManager.FindByIdAsync(id);

        if (user is null || user.IsDeleted)
            return Result.Failure(PatientErrors.PatientNotFound);

        string? imageUrl;

        if (request.Image is not null)
        {
            imageUrl = await fileService.UploadFileAsync(request.Image, "uploads/profiles");
        }
        else
            imageUrl = user.ImageUrl;

        mapper.Map(request, user);
        user.ImageUrl = imageUrl;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        await unitOfWork.SaveAsync();

        return Result.Success();

    }
    public async Task<Result> DeleteAsync(string id)
    {
        var patient = await unitOfWork.Patients.GetByIdAsync(id);


        if (patient is null)
            return Result.Failure(PatientErrors.PatientNotFound);

        if (patient.IsDeleted)
            return Result.Failure(PatientErrors.PatientAlreadyDeleted);

        patient.IsDeleted = true;
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<PaginatedList<PatientResposne>>> GetAllAsync(RequestFilters filters)
    {
        var query = unitOfWork.Patients.GetAllWithDetailsQueryable(filters)
            .ProjectTo<PatientResposne>(mapper.ConfigurationProvider);

        var result = await PaginatedList<PatientResposne>
                                      .CreateAsync(query, filters.PageNumber, filters.PageSize);

        //var mappedItems = mapper.Map<List<PatientResposne>>(paginatedPatients.Items);

        //var result = new PaginatedList<PatientResposne>(
        //    mappedItems,
        //    paginatedPatients.PageNumber,
        //    paginatedPatients.TotalCount,
        //    paginatedPatients.TotalPages
        //);

        return Result.Success(result);
    }

    public async Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatientsAsync()
    {
        return Result.Success(await unitOfWork.Patients.GetAllActiveAsync());
    }

    public async Task<Result<PatientResposne>> GetByIdAsync(string id)
    {
        var patient =await unitOfWork.Patients.GetByIdAsync(id);

        if (patient is null || patient.IsDeleted)
            return Result.Failure<PatientResposne>(PatientErrors.PatientNotFound);

        return Result.Success(mapper.Map<PatientResposne>(patient));
    }

    public Result DeleteMany(List<string> ids)
    {
        var idList = ids.Distinct().ToList();

        if (idList.Count == 0)
            return Result.Failure(PatientErrors.NoIdsProvided);

        unitOfWork.Patients.DeleteMany(idList);

        return Result.Success();
    }
}
