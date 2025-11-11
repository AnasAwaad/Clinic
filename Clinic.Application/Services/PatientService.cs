using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            user.ImageUrl = "/uploads/profiles/avatar.jpg";

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault();
            return Result.Failure<PatientResposne>(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest));
        }

        await userManager.AddToRoleAsync(user, AppRoles.Patient);

        await unitOfWork.SaveAsync();


        return Result.Success(mapper.Map<PatientResposne>(user));
    }

    public Task<Result> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PaginatedList<PatientResposne>>> GetAll(int pageNumber , int pageSize)
    {
        var items = unitOfWork.Patients.GetAllWithDetailsQueryable()
            .ProjectTo<PatientResposne>(mapper.ConfigurationProvider);

        var result = await PaginatedList<PatientResposne>.CreateAsync(items, pageNumber, pageSize);

        return Result.Success(result);
    }

    public async Task<Result<IEnumerable<PatientActiveResponse>>> GetAllActivePatients()
    {
        return Result.Success(await unitOfWork.Patients.GetAllActiveAsync());
    }

    public async Task<Result<PatientResposne>> GetById(string id)
    {
        var patient =await unitOfWork.Patients.GetByIdAsync(id);

        if (patient is null)
            return Result.Failure<PatientResposne>(PatientErrors.PatientNotFound);

        return Result.Success(mapper.Map<PatientResposne>(patient));
    }

    public Task<Result> Update(int id, PatientRequest request)
    {
        throw new NotImplementedException();
    }
}
