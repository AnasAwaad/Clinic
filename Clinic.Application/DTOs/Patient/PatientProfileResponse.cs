using Clinic.Application.DTOs.Appointment;
using Clinic.Application.DTOs.Prescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Patient;
public class PatientProfileResponse
{
    public PatientInfoResponse PatientInformation { get; set; } = default!;
    public IEnumerable<AppointmentHistoryResponse> AppointmentHistory { get; set; } = default!;
    public IEnumerable<PrescriptionHistoryResponse> PrescriptionHistory { get; set; } = default!;
}
