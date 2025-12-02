using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisitType",
                table: "Appointments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReasonForVisit",
                table: "Appointments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14ce8c78-3c61-42df-b9f5-57f56c554366", new DateTime(2025, 12, 2, 20, 21, 26, 810, DateTimeKind.Local).AddTicks(8944), "AQAAAAIAAYagAAAAENlGvQXq+xZxkaY/CErn4vrQu26fEcJZrItvbjZ2jORQzi4fa6nHDXoVwMXpLdrGCQ==", "7f9623b9-0a63-45bc-9137-88d58f9c5ea8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a24dc94-c683-4b4e-84b9-be9551cf3252", new DateTime(2025, 12, 2, 20, 21, 26, 700, DateTimeKind.Local).AddTicks(7251), "AQAAAAIAAYagAAAAEHFILaTyR0Na+s+OMisycrbw+toTQWEZu43ZI93+fEBdL9UO0aUVulP5f7zJyMMW5Q==", "faf73ec6-d87d-47f9-ad0e-62ae25a139d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf1a9a85-24e8-4ec7-984c-8bc6627a3880", new DateTime(2025, 12, 2, 20, 21, 26, 601, DateTimeKind.Local).AddTicks(4101), "AQAAAAIAAYagAAAAEFQq2oUCOFU3jYUtmIJpoAg6FP6nYAn/r10I06+u62c/i1ekrO2FXInGxqut/x/gBA==", "bf50ae97-b3c2-4631-9d62-5c3995b2ec3b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VisitType",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ReasonForVisit",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cc5e083-5722-4c4c-adcb-b028ea896376", new DateTime(2025, 11, 30, 17, 35, 7, 700, DateTimeKind.Local).AddTicks(9418), "AQAAAAIAAYagAAAAEGiXIOQR0JVXQ+HqYieJncs6DMNGT3+D9UPI9no+QORh0dw3mFn26dwAJ2BbEHLJzw==", "510eab40-0f8f-4a54-a0cf-916157b1077e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1589b135-88bd-4a2a-bcf1-1b5b980e5c3f", new DateTime(2025, 11, 30, 17, 35, 7, 600, DateTimeKind.Local).AddTicks(4771), "AQAAAAIAAYagAAAAEGhno1earc7KRy+2euuGH1AzGt+wxkkTBAZij9A3vOtPqg43CNv9jusjBa0T6ytm9w==", "85a61fc4-83b4-42e4-a47c-78b9c0a98eb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e789bea-c44c-4b0a-a953-bf6e8856887a", new DateTime(2025, 11, 30, 17, 35, 7, 505, DateTimeKind.Local).AddTicks(647), "AQAAAAIAAYagAAAAEP4RGZKgxaXzhFVkSNH91QR+c7DOlKIjAH/UJghVs8XRXKSNgkHHtoiUCthihtmp5w==", "46accb24-9f86-4b49-8108-852953456770" });
        }
    }
}
