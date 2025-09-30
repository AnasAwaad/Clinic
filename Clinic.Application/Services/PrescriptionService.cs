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
    public async Task<Result<PrescriptionResponse>> CreateAsync(string userId, int recordId, PrescriptionRequest request)
    {
        var recordIsExists = await unitOfWork.MedicalRecords.MedicalRecordIsExists(recordId);
        if(!recordIsExists)
            return Result.Failure<PrescriptionResponse>(MedicalRecordErrors.MedicalRecordNotFound);

        var patient = await unitOfWork.Patients.GetByUserIdAsync(userId);

        if (patient is null)
            return Result.Failure<PrescriptionResponse>(PatientErrors.PatientNotFound);

        var prescription = mapper.Map<Prescription>(request);
        prescription.MedicalRecordId = recordId;

        await unitOfWork.Prescriptions.AddAsync(prescription);
        await unitOfWork.SaveAsync();

        return Result.Success(mapper.Map<PrescriptionResponse>(prescription));
    }

    public async Task<Result> DeleteAsync(int recordId, int id)
    {
        var recordIsExists = await unitOfWork.MedicalRecords.MedicalRecordIsExists(recordId);
        if (!recordIsExists)
            return Result.Failure(MedicalRecordErrors.MedicalRecordNotFound);

        var prescription = await unitOfWork.Prescriptions.GetByIdAsync(id);

        if (prescription is null)
            return Result.Failure(PrescriptionErrors.PrescriptionNotFound);

        await unitOfWork.Prescriptions.DeleteAsync(prescription);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<PrescriptionResponse>>> GetAllAsync(int recordId)
    {
        var recordIsExists = await unitOfWork.MedicalRecords.MedicalRecordIsExists(recordId);
        if (!recordIsExists)
            return Result.Failure<IEnumerable<PrescriptionResponse>>(MedicalRecordErrors.MedicalRecordNotFound);

        var prescriptions = await unitOfWork.Prescriptions.GetAllByMedicalRecordAsync(recordId);

        return Result.Success(mapper.Map<IEnumerable<PrescriptionResponse>>(prescriptions));
    }

    public async Task<Result<PrescriptionResponse>> GetByIdAsync(int recordId, int id)
    {
        var prescription = await unitOfWork.Prescriptions.GetByMedicalRecordAsync(recordId,id);

        if(prescription is null)
            return Result.Failure<PrescriptionResponse>(PrescriptionErrors.PrescriptionNotFound);

        return Result.Success(mapper.Map<PrescriptionResponse>(prescription));
    }

    public async Task<Result> UpdateAsync(int recordId, int id, PrescriptionRequest request)
    {
        var recordIsExists = await unitOfWork.MedicalRecords.MedicalRecordIsExists(recordId);
        if (!recordIsExists)
            return Result.Failure<IEnumerable<PrescriptionResponse>>(MedicalRecordErrors.MedicalRecordNotFound);

        var prescription = await unitOfWork.Prescriptions.GetByIdAsync(id);

        mapper.Map(request, prescription);

        await unitOfWork.SaveAsync();

        return Result.Success();
    }
}
