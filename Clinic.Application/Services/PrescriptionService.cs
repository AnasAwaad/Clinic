using AutoMapper;
using AutoMapper.QueryableExtensions;
using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using Clinic.Application.Interfaces.Repositories;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Services;
internal class PrescriptionService(IUnitOfWork unitOfWork,IMapper mapper) : IPrescriptionService
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

        await unitOfWork.Prescriptions.DeleteAsync(prescription);
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
            await unitOfWork.PrescriptionItems.DeleteAsync(item);


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
