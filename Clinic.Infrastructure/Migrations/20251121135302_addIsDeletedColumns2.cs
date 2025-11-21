using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeletedColumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_CreatedById",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_CreatedById",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "PrescriptionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PrescriptionItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PrescriptionItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PrescriptionItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "PrescriptionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "PrescriptionItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d119db65-61a0-4889-80c2-122380d242f5", "AQAAAAIAAYagAAAAENFzVe5Jx3Z4GvS6fd97kJgpdoHqpXpNEGX7Q78cvYqyUeySv29FZN5UW15EJok4mg==", "28975f71-d999-476b-8539-73eb6a83ce8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ca7032d-89ca-4cc7-971d-4d7eba94582a", "AQAAAAIAAYagAAAAEPGKdJygp1/ulL6yAIRoRYYsDEc/w3aHZyQks9F5+XogcRwMVpRGpnOeZJkPwUpDiw==", "b0502459-10cd-4691-b27e-56e8d6132e8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5b0321e-173a-409e-9e7f-0c4f143bff77", "AQAAAAIAAYagAAAAEPLN66diRDPcuJkS0TlaJri7O2FdRu1w3sChO8O/TWEsFPBmrPlZoTOiRc1duxtICg==", "864dabe8-9cc5-4061-b0a9-7bfde07369bd" });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_CreatedById",
                table: "PrescriptionItems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItems_UpdatedById",
                table: "PrescriptionItems",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_CreatedById",
                table: "MedicalRecords",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionItems_AspNetUsers_CreatedById",
                table: "PrescriptionItems",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionItems_AspNetUsers_UpdatedById",
                table: "PrescriptionItems",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_CreatedById",
                table: "Prescriptions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_CreatedById",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionItems_AspNetUsers_CreatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionItems_AspNetUsers_UpdatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_CreatedById",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionItems_CreatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionItems_UpdatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "PrescriptionItems");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "PrescriptionItems");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "MedicalRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_CreatedById",
                table: "Appointments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_AspNetUsers_CreatedById",
                table: "MedicalRecords",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_CreatedById",
                table: "Prescriptions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
