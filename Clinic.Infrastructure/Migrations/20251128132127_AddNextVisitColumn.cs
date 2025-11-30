using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNextVisitColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextVisit",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ada51ec-a011-4262-ae2c-81a710634f5e", "AQAAAAIAAYagAAAAEI4xDqB7YgOcEGpPtwNVHMZrt+Hoys9MHwBCU0wWbFALVX9a7VYH8OZQ5m1tyghBBA==", "cfd0b94f-60c2-4f42-b1e6-6078736dab57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a0d77fb-01cd-4749-95a8-81f8a4894825", "AQAAAAIAAYagAAAAEJNIiaDqwQwRfnt5VgETxoUBb/uWP4Wmd3M5k+0s/zi+Vl/2IJmuiU4SsDzWmj+IJQ==", "85111a25-8dad-4d78-be8f-4450b814d786" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "960297dd-8976-4bcd-a1cb-34d34b80d9ed", "AQAAAAIAAYagAAAAEF6G4a4pZQB1YdQj5WL26UD+D9GoF+exYYxcmcRMrTzwnAFsEgZolmeiz4KYVQehGA==", "eeaa8111-991e-4d88-91a4-94b781b960c9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextVisit",
                table: "Prescriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "20d50f7d-c4cb-432c-8b33-00d0be480dae", "AQAAAAIAAYagAAAAEDA3LGO1UCEdOI32j9Kn2f5qarw3RJfpgfBccRNKXKq5CkU4cNgYpOrIYig1oum+Rg==", "07d283de-ef53-4afb-8c22-baf11dc263d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8680ddc2-d39a-41b4-9cb3-d0dc34d29708", "AQAAAAIAAYagAAAAEMMgqpz9vGrgbTFmeBvuLwJ1xN0J0uwmJz/AOl1VoMJAQBJqWLH38d16IbCJcPrEoA==", "b2303bc6-6311-4036-851f-cae07c3b6d62" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fa771ca3-a9cc-492f-8109-8bec8c38913b", "AQAAAAIAAYagAAAAEIG5s81etlP4CKbWdbtOT/Yil3RkMZ02qqgzWhpQi5hribuyH/eTiya1j91Wy7jbWA==", "33666ec9-580c-4814-90e6-7797aa01cf23" });
        }
    }
}
