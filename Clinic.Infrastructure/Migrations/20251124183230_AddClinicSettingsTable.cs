using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClinicSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClinicSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Monday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Monday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Monday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Tuesday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Tuesday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Tuesday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Wednesday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Wednesday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Wednesday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Thursday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Thursday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Thursday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Friday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Friday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Friday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Saturday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Saturday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Saturday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    WorkHours_Sunday_Open = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Sunday_Close = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkHours_Sunday_IsClose = table.Column<bool>(type: "bit", nullable: true),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorRegNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicSettings", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicSettings");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c09c65a6-c614-40ff-829c-2b1906ba3623", "AQAAAAIAAYagAAAAEJGbPmnmWzUvRfiILXso4AtPAreyoPmkYTUrLtrLNjCtExfr9iU/qsl+KNFsZCOVTA==", "6e60d5ac-b948-46d0-996c-add131d3cf7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee6326de-4810-43dc-b3b6-61dd6ab45003", "AQAAAAIAAYagAAAAEE44tRnKKGcybm5h/KwLsubRGu0B133WdA3FThxhOM1ZJG9xF/CRWAybZHqutDnfrw==", "9ef15e20-f92b-4df6-b9fe-50c71182f6b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4752f689-d75e-4af0-9b18-ee6a7ce6704d", "AQAAAAIAAYagAAAAEDoeNl47xdESzrdLpS+8Ac5CFTRznWIkodWeGGuXIMC2zVFtV7r7SyVAajaHluxg6A==", "dcca8150-e8f5-4899-86cf-463b09d0c066" });
        }
    }
}
