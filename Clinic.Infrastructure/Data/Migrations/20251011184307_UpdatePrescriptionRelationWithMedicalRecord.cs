using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrescriptionRelationWithMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalRecordId",
                table: "Prescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3662825-fb30-4c17-9d99-02f4a18784b6", "AQAAAAIAAYagAAAAEHCoDwINegul1tjd8hI4OCXaOvXAfi8ybrHUYixJd60azuN6KjecFWzd1kq6Ztk1Pw==", "cf6809c0-f736-4de5-b055-63ff74b33bf7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdd73b3e-b98a-4ed6-b5ec-dd1069b86e72", "AQAAAAIAAYagAAAAEEPSCF99SzGVwEhESEd1HUFqJ58UB2pjGMucX3YdBk5H5PY4q/e+OAk8xrHz3UzD+A==", "bdfe1893-d0c2-4191-b174-f4a01acf19be" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99851408-2dec-439d-aec5-c290eba51706", "AQAAAAIAAYagAAAAEF3cUpPnp15vFwhAAg9Efgu4bvEaxeoVZW13ZHGNcAz8nLFau5WIKU1AZu7NYVf6Gw==", "8d47254e-f281-4b36-9ca0-3d8cff3e5860" });

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                table: "Prescriptions",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalRecordId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bb03b67-a702-4c43-9579-3883928c6e54", "AQAAAAIAAYagAAAAEImoLFS2Y4cMwFthvJ4mlDRoLv/+56TsV7xIu5CxX5Z+E8zbjmfi8Vd5YAFA9Gj4zA==", "ec81777a-0773-4e1c-8203-e556b4c4cb24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa5d0213-9315-48f9-af73-4bba71942d79", "AQAAAAIAAYagAAAAEEskRDZLagGdj7YOSFZFlOtGI88PpCF+8erEQjRrqBFWgUhdXBqgSUlaKzfND1LTfw==", "351b92ac-7673-4397-bf58-cc3374bab891" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ce2b596-edd5-45d9-8cef-d3f57e012d2c", "AQAAAAIAAYagAAAAECyvWSVjhoxsNZUTomyB6A+Y/yZIf+3v5UF5Sv2STzBu8q5ICOUWFD2bvhufO+sXqA==", "0c317776-aeb6-464f-84e4-7bc76c5aab4a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_MedicalRecords_MedicalRecordId",
                table: "Prescriptions",
                column: "MedicalRecordId",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
