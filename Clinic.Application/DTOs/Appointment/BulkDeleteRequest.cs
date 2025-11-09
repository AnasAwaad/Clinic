using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.DTOs.Appointment;
public class BulkDeleteRequest
{
    public List<int> Ids { get; set; } = new();
}