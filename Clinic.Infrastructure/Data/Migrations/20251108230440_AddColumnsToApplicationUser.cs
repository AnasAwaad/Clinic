using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                column: "ClaimValue",
                value: "appointments:read-own");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "ClaimValue",
                value: "appointments:cancel");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "timeslots:read");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "Address", "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "", "a5c7770a-686f-44bb-ad80-fe7a2b7a3ae7", "", "AQAAAAIAAYagAAAAEAnb9EqhgZ33SR90XoKtrnS2JTUG3ylwltGIckgZwgbdrj209BHpsgP8gjxYIDHrrg==", "1b4bc870-7828-46b5-9178-6914591076fc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "Address", "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "", "8e000a17-f1df-4d9f-aae9-aee765336be5", "", "AQAAAAIAAYagAAAAEDq0fn9V9+nd7BK1fuyb+8J8E2UhWSZCVUF/ubfzEL9GSW8l3iPrVyYZV9qy129tAg==", "8dd1d743-0696-49fc-8fae-c2a92752f695" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "Address", "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "", "1b342f15-2b71-4ee7-a797-fa84b3374702", "", "AQAAAAIAAYagAAAAEM/tUD4BVuip0oPZAtvVGCRPlQpqMAgmaDkxeIZttPyfzxXJZPBacy2FjyH94i6tWg==", "3ec8a719-9971-4cd6-bfc5-efd295230d80" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40,
                column: "ClaimValue",
                value: "appointments:read");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41,
                column: "ClaimValue",
                value: "appointments:read-own");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42,
                column: "ClaimValue",
                value: "appointments:cancel");

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 43, "permissions", "timeslots:read", "f4c051d8-f995-492c-8ef8-515417105616" });

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
    }
}
