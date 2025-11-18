using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropToTimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookedAt",
                table: "TimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "076e0a69-d8a5-4ac2-a7ef-9d80b59ed387", "AQAAAAIAAYagAAAAEHkAejOBx6m66Xth9dP6gezrmVh/6Yf+4LJ1fWL7K5uwIvBpWhW0xBxR84OlKo2dvw==", "e1fc1482-5c1a-4d30-a95e-0c08ec4d50cc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51fb311a-331e-4ccb-90e9-671de3c6b341", "AQAAAAIAAYagAAAAEJUUD68B6d00K9oHLmYaoo91MVtGttGxdX0AOP4iraxud8XqsChkXG40U8nqv/oJtw==", "36bbeb50-937b-4d5f-89dd-476a4c8367cd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a85b6f7-2b93-484d-a3ff-df7541ce76a2", "AQAAAAIAAYagAAAAEBz5x3+Pz/vQGQ2w872b+KYirzKMp/B7L/Wn8ks5xtnEFcOObRP+VVO+S1n8QfBSpw==", "0a5b1846-7402-470a-805a-85c20b364e37" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedAt",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TimeSlots");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e52ae181-6abf-4107-9e65-b4f47f8d5574", "AQAAAAIAAYagAAAAEI78IK89vn6pn4MvcYLONgvWPIi+DjZZNuosj3GvUFCs5u71sVfo+wS4USKfHmbIBg==", "a32dc5bd-7c99-44f0-aa85-083e9f6691e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9651decb-4a5f-4a1c-a5ba-4c00ca8419d5", "AQAAAAIAAYagAAAAEA+ZxcuXBZ77/gDlWCN0JAliTyuymysNd5IgIfyvQM3XzT5gX9Ki/d3hgmoHG0qTfQ==", "57f40cc6-3cf5-435c-b414-ec60e6055a90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8f3c837-abb8-424e-b8c7-269853e36ebb", "AQAAAAIAAYagAAAAEM3PuNDStTAirar/Uz79y8ptAEnpnMXypFLArMH+c17lpNaybfXcyhOTFn+XF7Qjcw==", "09e8959e-e3a4-4c1c-a1c6-2641713a4969" });
        }
    }
}
