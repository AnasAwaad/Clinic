using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "MedicalRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MedicalRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d660974e-4d2e-49ab-9a94-ba4c79d22425", "AQAAAAIAAYagAAAAEAfG31okg8f0cFjUjulpceJFQKz3qKO4Ia0gKNEo68PByaSQ3VhfraJqCNmeXHVKqw==", "262b6d66-43f0-42a7-8f06-18437daeadf8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88f8adf5-41a0-4440-b5ec-c1c11309168e", "AQAAAAIAAYagAAAAEJbcL34Tlx2IVJAjEKFKok0fSfsARcfJxDkk/OWfP0eZ0JiNqRualJ+mTCdVjmomuA==", "75830b8a-8ccc-4636-88c8-227517c9e6c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f2695e7-0443-4cd6-a92f-ab6afd90047d", "AQAAAAIAAYagAAAAEOS65wSU4Q4AE1WUoZmV1fOQtYU2AR8gZ1Ius3BaewVEU3H06AGUr4H4wcofWt7smw==", "05cfceb3-4b6e-453a-ad25-0723c7055aa1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MedicalRecords");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0d223c2-c36a-4103-b581-d478cea63329", "AQAAAAIAAYagAAAAEHxjYctCLPPBbZXeGp7/lWv8mVJ8yJ0A74lXDWldpsiZG3Lm8B88pEI2+L2C2apCYg==", "bc224d0a-84ea-4904-be90-c22c58cba5a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "446237fd-8491-476b-a8fa-8a4de5c95781", "AQAAAAIAAYagAAAAEPwCXNPA09FPh1CHo1F16PfVlPYTtYZisYPU5Uku12CHm8YGFiV96EhdYiJ4IMzCkQ==", "76dc4133-9f6e-4d24-bca3-1c7382478fbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38be2e82-cb37-417b-9d22-208fc6ef1baa", "AQAAAAIAAYagAAAAEA8dhh7Ws7KCV4I/BFcmeeLfrIbVMi8aQOqXWP93DNDEMVomXQalePozMkAe2Io0yQ==", "c29d894b-dda3-4831-8119-e7118a59986b" });
        }
    }
}
