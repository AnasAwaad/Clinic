using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedOnDateToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

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
    }
}
