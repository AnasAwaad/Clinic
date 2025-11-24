using Clinic.Application.DTOs.Prescription;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace Clinic.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PdfTestController : ControllerBase
{
    [HttpGet("prescriptionPdf")]
    public IActionResult GenerateTestPdf()
    {
        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        // fetch prescription data from database
        var prescription = new PrescriptionPdfModel
        {
            // you can change these as needed
            Date = new DateTime(2019, 7, 25),
            PatientId = "14",
            PatientName = "DEMO PATIENT",
            PatientGenderAge = "M / 8 Y",
            NextVisit = new DateTime(2019, 8, 9),
            Items = new List<PrescriptionItemPdf>
            {
                new()
                {
                    Medicine = "TAB. DEMO MEDICINE 1",
                    Dosage = "1 सुबह, 1 रात (भोजन पचने के बाद)",
                    Duration = "10 दिन"
                },
                new()
                {
                    Medicine = "CAP. DEMO MEDICINE 2",
                    Dosage = "1 सुबह, 1 रात (भोजन पहले)",
                    Duration = "10 दिन"
                },
                new()
                {
                    Medicine = "TAB. DEMO MEDICINE 3",
                    Dosage = "1 सुबह, 1 दोपहर, 1 शाम, 1 रात",
                    Duration = "10 दिन"
                },
                new()
                {
                    Medicine = "TAB. DEMO MEDICINE 4",
                    Dosage = "1/2 सुबह, 1/2 रात",
                    Duration = "10 दिन"
                }
            }
        };



        var document = new PrescriptionDocument(prescription);
        var pdfBytes = document.GeneratePdf();

        return File(pdfBytes, "application/pdf", "prescription.pdf");
    }
}
