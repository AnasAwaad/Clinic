using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrescriptionItems",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PrescriptionItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1acd75b6-89b1-4539-a71e-ac3243524790", "AQAAAAIAAYagAAAAEHd/TXxDTcOqCVZ2uUSAwavs1UI5rT0s57rPkc+cH0PGwPymgsloW9dpY5erqje6dQ==", "14554f52-35a9-4a16-aef9-656ac42289d2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3757b90-27cc-4617-bcae-95f2050d0a4b", "AQAAAAIAAYagAAAAEJc1szk/bqxlO0EdqLFTXUxxTtcF4zMY/s8wt+z4Ptkjo7DMar3rCh6c8/j7QfkKgg==", "99542f00-5fab-43b2-a898-f6436fbb167f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed9fa9ee-0ff1-4827-85c5-87bd0a0d7168", "AQAAAAIAAYagAAAAEILRUKPfyb1/Abm7ODSsxLbaerpkqYnGZI8iB3u6LT7VTd4Ys/AXYsUpwU/6CY7LeA==", "5375c2c3-cf98-4ffa-ad2c-259331745392" });
        }
    }
}
