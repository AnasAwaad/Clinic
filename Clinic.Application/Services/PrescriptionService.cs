using AutoMapper;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class PrescriptionService(IUnitOfWork unitOfWork,IMapper mapper) : IPrescriptionService
{
    public async Task<Result<PrescriptionResponse>> CreateAsync(string userId, PrescriptionRequest request)
    {
        var patient = await unitOfWork.Patients.GetByUserIdAsync(userId);

        if (patient is null)
            return Result.Failure<PrescriptionResponse>(PatientErrors.PatientNotFound);

        var prescription = mapper.Map<Prescription>(request);

        await unitOfWork.Prescriptions.AddAsync(prescription);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<PrescriptionResponse>(prescription));
    }

    public async Task<Result> DeleteAsync( int id)
    {
        var prescription = await unitOfWork.Prescriptions.GetByIdAsync(id);

        if (prescription is null)
            return Result.Failure(PrescriptionErrors.PrescriptionNotFound);

        await unitOfWork.Prescriptions.DeleteAsync(prescription);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<PrescriptionResponse>>> GetAllAsync()
    {
       
        var prescriptions = await unitOfWork.Prescriptions.GetAllAsync();

        return Result.Success(mapper.Map<IEnumerable<PrescriptionResponse>>(prescriptions));
    }

    public async Task<Result<PrescriptionResponse>> GetByIdAsync(int id)
    {
        var prescription = await unitOfWork.Prescriptions.GetAllAsync();

        if(prescription is null)
            return Result.Failure<PrescriptionResponse>(PrescriptionErrors.PrescriptionNotFound);

        return Result.Success(mapper.Map<PrescriptionResponse>(prescription));
    }

    public async Task<Result> UpdateAsync( int id, PrescriptionRequest request)
    {
        var prescription = await unitOfWork.Prescriptions.GetByIdAsync(id);

        mapper.Map(request, prescription);

        await unitOfWork.SaveAsync();

        return Result.Success();
    }
}
