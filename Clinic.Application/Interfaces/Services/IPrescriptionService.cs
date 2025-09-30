using Clinic.Application.DTOs.MedicalRecord;
using Clinic.Application.DTOs.Prescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IPrescriptionService
{
    Task<Result<PrescriptionResponse>> CreateAsync(string userId,int recordId, PrescriptionRequest request);
    Task<Result<PrescriptionResponse>> GetByIdAsync(int recordId,int id);
    Task<Result<IEnumerable<PrescriptionResponse>>> GetAllAsync(int recordId);
    Task<Result> UpdateAsync(int recordId, int id, PrescriptionRequest request);
    Task<Result> DeleteAsync(int recordId, int id);
}
