using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrescriptionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "NextVisit",
                table: "Prescriptions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Prescriptions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f986595b-45d4-441d-8fdd-5ea716bada21", "AQAAAAIAAYagAAAAEM8ews0FQKuggsIxkPlAXBcjutF4dPrpYszGqZDdA88olYKxY4xbQOhFPgTnVdw4+g==", "3afc6580-50bf-4510-8366-3e317328752f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "846e42d0-cea7-4e23-ab38-a4019270e3a1", "AQAAAAIAAYagAAAAECxxWoU33cccYRPVFSc2uYBnAkeFeSv/24DocuZO5Ud7sAJW4iOl5Q6hb2OW7v+nOg==", "427e9ead-0a7b-4d7a-9c11-09a849f4e2a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ec293bd-3b70-4d22-ac04-85d9d6545bb9", "AQAAAAIAAYagAAAAEOXenDIeoY9Hs/fDQbFMQ4kum5QH61h1Zu8p+9npFy0bVVh718cM66pqSzOc4/tuMA==", "e49792b3-2c66-4264-8be0-e663d5f5950c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NextVisit",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d68085f5-bc42-4fac-b304-889fb51e7675", "AQAAAAIAAYagAAAAEJcxQCqlV3p5FsTGdapek3xzHSIav2fRi6uW315Qm88fbaC0JudnT58nHWSnH/Br4w==", "3710cce3-f985-4419-8ac3-61c41e30dc01" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "466cc22a-d207-4f7c-83e1-1b36415bf3d4", "AQAAAAIAAYagAAAAEHFYW8p067Wrr62KQ3DyYP5tISXBXgaFzTLrUFPBPINv2PjqYTTFP3ordHJp4wXc7g==", "38321c13-4c45-4e9a-ac1c-d9441fd9059b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c77bf4b6-8bab-4967-a012-3ccce9a90955", "AQAAAAIAAYagAAAAEP46+b9kok0kdGcbfFiAMNMC1wEHLp70tMXFQA+hSvBDoIGt4OjvsNrZDfqxbOiUkA==", "974ca241-8d02-44b2-abd0-175bac1a6f78" });
        }
    }
}
