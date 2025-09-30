using AutoMapper;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class MedicalRecordService(IUnitOfWork unitOfWork,IMapper mapper) : IMedicalRecordService
{
    public async Task<Result<MedicalRecordResponse>> CreateAsync(string userId, MedicalRecordRequest request)
    {
        var patient = await unitOfWork.Patients.GetByUserIdAsync(userId);

        if(patient is null)
            return Result.Failure<MedicalRecordResponse>(PatientErrors.PatientNotFound);

        var medicalRecord = mapper.Map<MedicalRecord>(request);
        medicalRecord.PatientId = patient.Id;

        await unitOfWork.MedicalRecords.AddAsync(medicalRecord);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<MedicalRecordResponse>(medicalRecord));
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var medicalRecord = await unitOfWork.MedicalRecords.GetByIdAsync(id);
        if (medicalRecord is null)
            return Result.Failure(MedicalRecordErrors.MedicalRecordNotFound);

        await unitOfWork.MedicalRecords.DeleteAsync(medicalRecord);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<MedicalRecordResponse>>> GetAllAsync()
    {
        var medicalRecords = await unitOfWork.MedicalRecords.GetAllAsync();

        return Result.Success(mapper.Map<IEnumerable<MedicalRecordResponse>>(medicalRecords));
    }

    public async Task<Result<MedicalRecordResponse>> GetByIdAsync(int id)
    {
        var medicalRecord = await unitOfWork.MedicalRecords.GetByIdAsync(id);
        if (medicalRecord is null)
            return Result.Failure<MedicalRecordResponse>(MedicalRecordErrors.MedicalRecordNotFound);

        return Result.Success(mapper.Map<MedicalRecordResponse>(medicalRecord));
    }

    public async Task<Result> UpdateAsync(int id, MedicalRecordRequest request)
    {
        var medicalRecord = await unitOfWork.MedicalRecords.GetByIdAsync(id);
        if (medicalRecord is null)
            return Result.Failure(MedicalRecordErrors.MedicalRecordNotFound);

        mapper.Map(request, medicalRecord);

        await unitOfWork.SaveAsync();
        return Result.Success();
    }
}
