using Clinic.Application.DTOs.Prescription;
using Clinic.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PrescriptionDocument(PrescriptionPdfModel model, IWebHostEnvironment webHostEnvironment) : IDocument
{
    

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A6);
            page.Margin(20);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    // ---------------- HEADER ------------------
    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            // Logo small
            if (!string.IsNullOrWhiteSpace(model.ClinicLogo))
            {
                var imagePath = Path.Combine(webHostEnvironment.WebRootPath, model.ClinicLogo.Replace('\\','/').TrimStart('/'));
                byte[] imageData = File.ReadAllBytes(imagePath);

                row.ConstantItem(45).Image(imageData, ImageScaling.FitArea);
            }

            // Doctor info
            row.RelativeItem().Column(col =>
            {
                col.Item().Text(model.DoctorName)
                    .Bold().FontSize(14).FontColor(Colors.Blue.Medium);

                col.Item().Text(model.DoctorDegrees)
                    .FontSize(8);

                if (!string.IsNullOrEmpty(model.DoctorRegNo))
                    col.Item().Text($"Reg: {model.DoctorRegNo}")
                        .FontSize(8);
            });

            // Clinic info
            row.RelativeItem().AlignRight().Column(col =>
            {
                col.Item().Text(model.ClinicName)
                    .Bold().FontSize(12);

                col.Item().Text(model.ClinicAddress)
                    .FontSize(8);

                if (!string.IsNullOrEmpty(model.ClinicPhone))
                    col.Item().Text($"Phone: {model.ClinicPhone}").FontSize(8);

                // ---- Opening Hours Table ----
                //if (model.WorkingHours is not null && model.WorkingHours.Count > 0)
                //{
                //    col.Item().PaddingTop(4).Text("Opening Hours:")
                //        .SemiBold()
                //        .FontSize(8);

                //    col.Item().Column(hoursCol =>
                //    {
                //        foreach (var h in model.WorkingHours)
                //        {
                //            hoursCol.Item()
                //                .Text($"{h.Day}: {h.From} - {h.To}")
                //                .FontSize(8);
                //        }
                //    });
                //}
            });

        });
    }

    // ---------------- CONTENT ------------------
    void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().PaddingVertical(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

            // Patient row
            col.Item().Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text($"{model.PatientName} ({model.PatientGenderAge})")
                        .SemiBold().FontSize(9);

                    c.Item().Text($"ID: {model.PatientId}").FontSize(8);

                    if (!string.IsNullOrEmpty(model.PatientAddress))
                        c.Item().Text(model.PatientAddress).FontSize(8);

                    c.Item().Text($"Dx: {model.Diagnosis}").FontSize(8);
                });

                row.ConstantItem(70).AlignRight()
                   .Text($"{model.Date:dd/MM/yyyy}")
                   .FontSize(9);
            });

            col.Item().PaddingVertical(4);


            // Table
            col.Item().PaddingTop(5).Element(ComposeTable);

            // Notes
            if (!string.IsNullOrEmpty(model.Notes))
            {
                col.Item().PaddingTop(4)
                   .Text("Notes:").SemiBold().FontSize(9);

                col.Item().Text(model.Notes).FontSize(8);
            }
        });
    }

    void ComposeTable(IContainer container)
    {
        container.Border(1)
                 .BorderColor(Colors.Grey.Darken2)
                 .Padding(0)
                 .Table(table =>
                 {
                     // Columns
                     table.ColumnsDefinition(columns =>
                     {
                         columns.RelativeColumn(5);  // Medicine
                         columns.RelativeColumn(3);  // Dose
                         columns.RelativeColumn(4);  // Regimen
                     });

                     // Header
                     table.Header(header =>
                     {
                         header.Cell().BorderRight(1).BorderColor(Colors.Grey.Darken2)
                            .Element(H).Text("Medicine");

                         header.Cell().BorderRight(1).BorderColor(Colors.Grey.Darken2)
                            .Element(H).Text("Dose");

                         header.Cell().Element(H).Text("Regimen");
                     });

                     // Rows
                     int i = 1;
                     foreach (var item in model.Items)
                     {
                         table.Cell()
                         .BorderTop(1).BorderRight(1)
                         .BorderColor(Colors.Grey.Darken2)
                         .Element(C).Text($"{i++}) {item.Name}").FontSize(8);

                         table.Cell()
                         .BorderTop(1).BorderRight(1)
                         .BorderColor(Colors.Grey.Darken2)
                         .Element(C).Text(item.Dosage).FontSize(8);

                         table.Cell()
                         .BorderTop(1)
                         .BorderColor(Colors.Grey.Darken2)
                         .Element(C).Text(BuildRegimen(item)).FontSize(8);
                     }
                 });

        // Header Style
        static IContainer H(IContainer c) =>
            c.Background(Colors.Grey.Lighten3)
             .DefaultTextStyle(x => x.SemiBold().FontSize(9))
             .Padding(4);

        // Cell Style
        static IContainer C(IContainer c) =>
            c.Padding(4);
    }

    private string BuildRegimen(PrescriptionItemPdf item)
    {
        if (item.Frequency <= 0 && item.Days <= 0)
            return string.Empty;

        string freqText = item.Frequency switch
        {
            1 => "once daily",
            2 => "twice daily",
            3 => "3 times daily",
            _ => $"{item.Frequency} times daily"
        };

        if (item.Days > 0)
            return $"{freqText} for {item.Days} days";

        return freqText;
    }

    // ---------------- FOOTER ------------------
    void ComposeFooter(IContainer container)
    {
        container.PaddingTop(10).AlignRight().Column(col =>
        {
            col.Item().Text("Signature").FontSize(8);
            col.Item().Text(model.DoctorName).FontSize(8);
        });
    }
}
