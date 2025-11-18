using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookedAtColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookedAt",
                table: "TimeSlots",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f42e1704-e6fe-425f-83b4-1fedfd68699d", "AQAAAAIAAYagAAAAENRr75XA/owdzpbQBzpepXxY6rYy48RM5BlKIHXisQObyccLzIPp2ZVYHDKfgK52ug==", "af650718-a09f-4f0f-8ef1-320f377cf171" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0055bca8-b29e-48af-8d2f-92838643912b", "AQAAAAIAAYagAAAAEAGMfz/SHGUsRJ59KzS0OBFEKWmYCXR1cCOvkYK9Cmg878qe9THPCOQLZ8gOGn1BrA==", "8e2d83e2-d443-4d06-952e-bb9aeafd8833" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5c24249-b6c4-4195-a678-ba5609933e2c", "AQAAAAIAAYagAAAAEDkHOTLCpiFvNfZMVfXkqAVHeRtdl2t9JYTyngTW1/CP0wSDTgXLBDXYUtNhjgxa6g==", "0a8db3b7-d600-49bd-b9e7-c97008f55f1e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BookedAt",
                table: "TimeSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
