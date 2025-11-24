using Clinic.Application.DTOs.Prescription;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Infrastructure.Services;
public class PrescriptionPdfGenerator(IWebHostEnvironment webHostEnvironment) : IPrescriptionPdfGenerator
{
    
    public byte[] Generate(PrescriptionPdfModel model)
    {
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
        var document = new PrescriptionDocument(model,webHostEnvironment);
        return document.GeneratePdf();
    }
}
