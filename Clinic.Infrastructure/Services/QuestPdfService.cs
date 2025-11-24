using Clinic.Application.DTOs.Prescription;
using Clinic.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class PrescriptionDocument : IDocument
{
    private readonly PrescriptionPdfModel model;

    public PrescriptionDocument(PrescriptionPdfModel model)
    {
        this.model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(40);
            page.Size(PageSizes.A4);

            page.Header().Element(ComposeHeader);
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(col =>
            {
                col.Item().Text(model.DoctorName).SemiBold().FontSize(16).FontColor(Color.FromRGB(0,0,255));
                col.Item().Text(model.DoctorDegrees).FontSize(10);
                col.Item().Text(model.DoctorRegNo).FontSize(10);
            });

            row.RelativeItem().AlignRight().Column(col =>
            {
                col.Item().Text(model.ClinicName).SemiBold().FontSize(14);
                col.Item().Text(model.ClinicAddress).FontSize(10);
                col.Item().Text($"Ph: {model.ClinicPhone}").FontSize(10);
                col.Item().Text(model.ClinicTiming).FontSize(10);
            });
        });
    }

    void ComposeContent(IContainer container)
    {
        container.Column(col =>
        {
            // Top line separator
            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

            // Patient & date row
            col.Item().PaddingTop(10).Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text($"ID: {model.PatientId} - {model.PatientName} ({model.PatientGenderAge})")
                            .SemiBold().FontSize(11);
                    c.Item().Text($"Address: {model.PatientAddress}").FontSize(10);
                    c.Item().Text($"Referred By: {model.ReferredBy}").FontSize(10);
                    c.Item().Text($"Diagnosis: {model.Diagnosis}").FontSize(10);
                });

                row.ConstantItem(140).AlignRight().Column(c =>
                {
                    c.Item().Text($"Date: {model.Date:dd-MM-yyyy}")
                            .AlignRight().FontSize(11);
                });
            });

            col.Item().PaddingVertical(5);

            // Rx symbol
            col.Item().Text("R").FontSize(16).SemiBold().Underline();

            // Medicines table
            col.Item().PaddingTop(5).Element(ComposeTable);

            // Advice
            col.Item().PaddingTop(10).Text("Advice Given:").SemiBold().FontSize(11);
            col.Item().Text(model.Advice).FontSize(10);

            col.Item().PaddingTop(10)
                .Text($"Next Visit: {model.NextVisit:dd-MM-yyyy}")
                .FontSize(10);
        });
    }

    void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            // columns: No+Name, Dosage, Duration
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(3);
                columns.RelativeColumn(2);
            });

            // header
            table.Header(header =>
            {
                header.Cell().Element(HeaderCellStyle).Text("Medicine Name");
                header.Cell().Element(HeaderCellStyle).Text("Dosage");
                header.Cell().Element(HeaderCellStyle).Text("Duration");
            });

            // rows
            int index = 1;
            foreach (var item in model.Items)
            {
                table.Cell().Element(CellStyle)
                    .Text($"{index++}) {item.Medicine}").FontSize(10);

                table.Cell().Element(CellStyle)
                    .Text(item.Dosage).FontSize(10);

                table.Cell().Element(CellStyle)
                    .Text(item.Duration).FontSize(10);
            }
        });

        static IContainer HeaderCellStyle(IContainer container) =>
            container.DefaultTextStyle(x => x.SemiBold())
                     .PaddingVertical(4)
                     .BorderBottom(1)
                     .BorderColor(Colors.Grey.Lighten2);

        static IContainer CellStyle(IContainer container) =>
            container.PaddingVertical(4)
                     .BorderBottom(0.5f)
                     .BorderColor(Colors.Grey.Lighten3);
    }

    void ComposeFooter(IContainer container)
    {
        container.PaddingTop(20).Row(row =>
        {
            row.RelativeItem();
            row.ConstantItem(200).AlignRight().Column(col =>
            {
                col.Item().Text("Signature").AlignRight().FontSize(10);
                col.Item().Text(model.DoctorName).AlignRight().FontSize(10);
                col.Item().Text(model.DoctorDegrees).AlignRight().FontSize(9);
            });
        });
    }
}