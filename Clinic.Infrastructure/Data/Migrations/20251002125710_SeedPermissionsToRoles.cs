using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissionsToRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "timeslots:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 2, "permissions", "timeslots:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 3, "permissions", "timeslots:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 4, "permissions", "timeslots:delete", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 5, "permissions", "medicalrecords:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 6, "permissions", "medicalrecords:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 7, "permissions", "medicalrecords:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 8, "permissions", "medicalrecords:delete", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 9, "permissions", "prescriptions:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 10, "permissions", "prescriptions:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 11, "permissions", "prescriptions:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 12, "permissions", "prescriptions:delete", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 13, "permissions", "appointments:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 14, "permissions", "appointments:read-own", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 15, "permissions", "appointments:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 16, "permissions", "appointments:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 17, "permissions", "appointments:cancel", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 18, "permissions", "appointments:delete", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 19, "permissions", "users:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 20, "permissions", "users:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 21, "permissions", "users:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 22, "permissions", "roles:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 23, "permissions", "roles:add", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 24, "permissions", "roles:update", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 25, "permissions", "results:read", "92787aec-1266-4a2d-8a2d-6ea48f5a4811" },
                    { 26, "permissions", "medicalrecords:read", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 27, "permissions", "medicalrecords:add", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 28, "permissions", "medicalrecords:update", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 29, "permissions", "prescriptions:read", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 30, "permissions", "prescriptions:add", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 31, "permissions", "prescriptions:update", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 32, "permissions", "prescriptions:delete", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 33, "permissions", "appointments:read", "0d1fe96c-7786-4ce6-8647-38da6886a662" },
                    { 34, "permissions", "appointments:read", "e6a5b8c2-6254-4279-866b-c916377576db" },
                    { 35, "permissions", "appointments:add", "e6a5b8c2-6254-4279-866b-c916377576db" },
                    { 36, "permissions", "appointments:cancel", "e6a5b8c2-6254-4279-866b-c916377576db" },
                    { 37, "permissions", "timeslots:read", "e6a5b8c2-6254-4279-866b-c916377576db" },
                    { 38, "permissions", "users:read", "e6a5b8c2-6254-4279-866b-c916377576db" },
                    { 39, "permissions", "appointments:add", "f4c051d8-f995-492c-8ef8-515417105616" },
                    { 40, "permissions", "appointments:read", "f4c051d8-f995-492c-8ef8-515417105616" },
                    { 41, "permissions", "appointments:read-own", "f4c051d8-f995-492c-8ef8-515417105616" },
                    { 42, "permissions", "appointments:cancel", "f4c051d8-f995-492c-8ef8-515417105616" },
                    { 43, "permissions", "timeslots:read", "f4c051d8-f995-492c-8ef8-515417105616" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92787aec-1266-4a2d-8a2d-6ea48f5a4811",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });

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
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fa5d0213-9315-48f9-af73-4bba71942d79", "Admin@gmail.com", "Admin", "", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEEskRDZLagGdj7YOSFZFlOtGI88PpCF+8erEQjRrqBFWgUhdXBqgSUlaKzfND1LTfw==", "351b92ac-7673-4397-bf58-cc3374bab891", "Admin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ce2b596-edd5-45d9-8cef-d3f57e012d2c", "AQAAAAIAAYagAAAAECyvWSVjhoxsNZUTomyB6A+Y/yZIf+3v5UF5Sv2STzBu8q5ICOUWFD2bvhufO+sXqA==", "0c317776-aeb6-464f-84e4-7bc76c5aab4a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92787aec-1266-4a2d-8a2d-6ea48f5a4811",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4cbe59c4-655e-40c0-8ea3-70808dc2ed94", "AQAAAAIAAYagAAAAEKVk45ul5ac7BLuBI9n7BQoe6ub6hFuWWkKGSXBMJFP/uUGIhWptFuL5jwQP6ANlcw==", "c2519875-25a7-471a-87ca-01abdd03b709" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "e7ac5cea-1980-4a87-9a55-f042e9b7a250", "SuperAdmin@gmail.com", "Super", "Admin", "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEDeQAhQk2nHxJoiok2KcnxdxolYPBHfPc4SFEcfxN9rzRdAVUtcuHGwudqgYYpHq6Q==", "dbef0310-28a7-4fa4-948b-dc062261e252", "SuperAdmin" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8c16dae-e626-4b30-bb0a-703226e61aad", "AQAAAAIAAYagAAAAELmo7xuF4iA8LIjVy6iasqZAf33CZVoycNHcjtj+pVeQ1CXUMfT/cpX/hjPmSu7Q1g==", "6b005e9b-8a11-494e-88ad-7d4bb3ee397b" });
        }
    }
}
