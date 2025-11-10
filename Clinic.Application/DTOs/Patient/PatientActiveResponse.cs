using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Patient;
public class PatientActiveResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
