using Clinic.Application.DTOs.Prescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Interfaces.Services;
public interface IPrescriptionPdfGenerator
{
    byte[] Generate(PrescriptionPdfModel model);
}
