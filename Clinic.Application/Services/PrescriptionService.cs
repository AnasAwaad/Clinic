using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class PrescriptionService(IUnitOfWork unitOfWork,IMapper mapper,IPrescriptionPdfGenerator prescriptionPdfGenerator) : IPrescriptionService
{
    public async Task<Result<PrescriptionResponse>> CreateAsync(PrescriptionRequest request)
    {
        var patient = await unitOfWork.Patients.GetByIdAsync(request.PatientId);

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

        prescription.IsDeleted = true;
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<PaginatedList<PrescriptionListResponse>>> GetAllAsync(string? searchValue,int pageNumber,int pageSize)
    {
        var items = unitOfWork.Prescriptions.GetAllWithItemsQueryable(searchValue)
            .ProjectTo<PrescriptionListResponse>(mapper.ConfigurationProvider);

        var result = await PaginatedList<PrescriptionListResponse>.CreateAsync(items, pageNumber, pageSize);

        return Result.Success(result);
    }

    public async Task<Result<PrescriptionResponse>> GetByIdAsync(int id)
    {
        var prescription = await unitOfWork.Prescriptions.GetByIdWithItemsAsync(id);

        if(prescription is null)
            return Result.Failure<PrescriptionResponse>(PrescriptionErrors.PrescriptionNotFound);

        return Result.Success(mapper.Map<PrescriptionResponse>(prescription));
    }

    public async Task<Result<byte[]>> PrintPrescriptionPdf(int id)
    {
        var prescription = await unitOfWork.Prescriptions.GetByIdWithItemsAsync(id);

        if (prescription is null)
            return Result.Failure<byte[]>(PrescriptionErrors.PrescriptionNotFound);

        var settings = await unitOfWork.ClinicSettings.GetByIdAsync(1);

        if (settings is null)
            return Result.Failure<byte[]>(new Error("NotFountClinicSettings", "Clinic settings not configured", StatusCodes.Status400BadRequest));

        var model = MapToPdfModel(prescription, settings);
        return Result.Success(prescriptionPdfGenerator.Generate(model));
    }

    private PrescriptionPdfModel MapToPdfModel(Prescription prescription,ClinicSettings settings)
    {
        return new PrescriptionPdfModel
        {
            PatientId = prescription.PatientId,
            ClinicName = settings.ClinicName,
            ClinicAddress = settings.ClinicAddress,
            ClinicPhone = settings.ClinicPhone,
            ClinicLogo = settings.LogoUrl,
            ClinicTiming = BuildClinicTiming(settings),
            DoctorName = settings.DoctorName,
            DoctorDegrees = settings.DoctorDegree,
            DoctorRegNo = settings.DoctorRegNo,
            Date = prescription.Date,
            PatientName = prescription.Patient.FullName,
            PatientGenderAge = $"{prescription.Patient.Gender} / {prescription.Age} Y",
            PatientAddress = prescription.Patient.Address,
            Diagnosis = prescription.Diagnosis,
            Notes = prescription.Notes,
            Items = prescription.Items.Select(x => new PrescriptionItemPdf
            {
                Days = x.Days,
                Dosage = x.Dosage,
                Frequency = x.Frequency,
                Instructions = x.Instructions,
                Name = x.Name
            }).ToList(),
        };
    }

    private string BuildClinicTiming(ClinicSettings settings)
    {
        return $"Timing: {settings.WorkHours.Monday.Open} - {settings.WorkHours.Monday.Close}";
    }

    public async Task<Result> UpdateAsync(int id, PrescriptionRequest request)
    {
        var patient = await unitOfWork.Patients.GetByIdAsync(request.PatientId);

        if (patient is null)
            return Result.Failure<PrescriptionResponse>(PatientErrors.PatientNotFound);

        var prescription = await unitOfWork.Prescriptions.GetByIdWithItemsAsync(id);

        if (prescription is null)
            return Result.Failure<PrescriptionResponse>(PrescriptionErrors.PrescriptionNotFound);

        //var items = mapper.Map<List<PrescriptionItem>>(request.Items);


        //var newItems = items.Except(prescription.Items).ToList();

        //var newItems = (from item in items
        //                where !(from p in prescription.Items select p.Id).Contains(item.Id)
        //                select item).ToList();

        var newItems = request.Items.Where(i => !prescription.Items.Any(p => p.Id == i.Id)).ToList();

        //var exitingItems = (from item in items
        //                    where (from p in prescription.Items select p.Id).Contains(item.Id)
        //                    select item).ToList();

        var exitingItems = request.Items.Where(i => prescription.Items.Any(p => p.Id == i.Id)).ToList();

        //var deletedItems = (from item in prescription.Items
        //                    where !(from i in items select i.Id).Contains(item.Id)
        //                    select item).ToList();

        var deletedItems = prescription.Items.Where(p => !request.Items.Any(i => i.Id == p.Id)).ToList();

        foreach (var item in newItems)
            prescription.Items.Add(mapper.Map<PrescriptionItem>(item));

        foreach (var item in deletedItems)
            item.IsDeleted = true;


        foreach (var item in exitingItems)
        {
            var oldItem = prescription.Items.First(i => i.Id == item.Id);
            oldItem.Name = item.Name;
            oldItem.Dosage = item.Dosage;
            oldItem.Days = item.Days;
            oldItem.Instructions = item.Instructions;
            oldItem.Frequency = item.Frequency;
        }

        prescription.PatientId = request.PatientId;
        prescription.Age = request.Age;
        prescription.Diagnosis = request.Diagnosis;
        prescription.Notes = request.Notes;
        prescription.Date = request.Date;


        await unitOfWork.SaveAsync();

        return Result.Success();
    }
}
