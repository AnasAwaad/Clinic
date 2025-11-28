using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using Clinic.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IPrescriptionService
{
    Task<Result> CreateAsync(PrescriptionRequest request);
    Task<Result<PrescriptionResponse>> GetByIdAsync(int id);
    Task<Result<PaginatedList<PrescriptionListResponse>>> GetAllAsync(string? searchValue ,int pageNumber,int pageSize);
    Task<Result> UpdateAsync(int id, PrescriptionRequest request);
    Task<Result> DeleteAsync(int id);
    Task<Result<byte[]>> PrintPrescriptionPdf(int id);
}
