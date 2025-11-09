using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedColumnsToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c324086c-eca4-4e59-89be-204789c8ff93", "AQAAAAIAAYagAAAAEJslWNSyrP6lEiDBz1CSkkbPOYJJYdJkUvh9rxOBrM2zmArnksswjR9f8nDte9fqOw==", "57e7af20-2ae2-47a3-a752-b29b1bd9b6cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de5155e9-037d-4e83-9a78-1197a77ea0ad", "AQAAAAIAAYagAAAAEMZafmp34LJIwk91eq/bAyoeDzKQ4Mhm8BR4obJKRMNIWbmtfj9iH4XVRFtV3ynhSA==", "ef311d18-1678-4d3f-b84a-f05e134547f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3c85c21-1888-4179-9631-f12c639cedfb", "AQAAAAIAAYagAAAAEIss2KnwfAz6wHD+ko5h76MkHjS4kUz5y7ZZX4tM4nAnmK8HVdF3hg5j7LTzAhn5YA==", "b2422ed9-8f2a-40c0-a5ee-519af5a6ac23" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5c7770a-686f-44bb-ad80-fe7a2b7a3ae7", "AQAAAAIAAYagAAAAEAnb9EqhgZ33SR90XoKtrnS2JTUG3ylwltGIckgZwgbdrj209BHpsgP8gjxYIDHrrg==", "1b4bc870-7828-46b5-9178-6914591076fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e000a17-f1df-4d9f-aae9-aee765336be5", "AQAAAAIAAYagAAAAEDq0fn9V9+nd7BK1fuyb+8J8E2UhWSZCVUF/ubfzEL9GSW8l3iPrVyYZV9qy129tAg==", "8dd1d743-0696-49fc-8fae-c2a92752f695" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b342f15-2b71-4ee7-a797-fa84b3374702", "AQAAAAIAAYagAAAAEM/tUD4BVuip0oPZAtvVGCRPlQpqMAgmaDkxeIZttPyfzxXJZPBacy2FjyH94i6tWg==", "3ec8a719-9971-4cd6-bfc5-efd295230d80" });
        }
    }
}
