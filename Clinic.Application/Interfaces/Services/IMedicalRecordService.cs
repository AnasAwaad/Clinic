using Clinic.Application.DTOs.MedicalRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IMedicalRecordService
{
    Task<Result<MedicalRecordResponse>> CreateAsync(string userId,MedicalRecordRequest request);
    Task<Result<MedicalRecordResponse>> GetByIdAsync(int id);
    Task<Result<IEnumerable<MedicalRecordResponse>>> GetAllAsync();
    Task<Result> UpdateAsync(int id, MedicalRecordRequest request);
    Task<Result> DeleteAsync(int id);
}
