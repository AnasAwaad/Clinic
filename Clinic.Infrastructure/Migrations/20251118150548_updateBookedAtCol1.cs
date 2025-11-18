using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBookedAtCol1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextVisit",
                table: "Prescriptions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43edaffa-d755-4d60-9825-aed549b1a3f4", "AQAAAAIAAYagAAAAEOFtJ39LEjEuaUa0PRG6+h5pAno7do1ydXxeiprVyQ0b7RUYd4V+Up6cxfdUC4fTPQ==", "1280fd08-edd0-4227-afcd-680ff96489c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7940de75-c3c3-40b4-8c2f-794560db782c", "AQAAAAIAAYagAAAAEK3RyUvjPN+E0B6OI+lLD+wj8EZAA2R+4HRnedftNa63kwe+6+FSBL5fRzbW7xhnNg==", "a18bd741-a03a-454d-9c42-35ba3fe557cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "beeb6da0-13fb-41e7-86c5-c187b0313225", "AQAAAAIAAYagAAAAEGgCvKJDm5GWvZ1b2UeyZ/RCiSu/p4Wnq6NNUk9Wg6JTUhXRUWxVgjlhtDmtrYTOoQ==", "6fc4e788-78a9-40a5-a614-e8ca14d69efa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "NextVisit",
                table: "Prescriptions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f42e1704-e6fe-425f-83b4-1fedfd68699d", "AQAAAAIAAYagAAAAENRr75XA/owdzpbQBzpepXxY6rYy48RM5BlKIHXisQObyccLzIPp2ZVYHDKfgK52ug==", "af650718-a09f-4f0f-8ef1-320f377cf171" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0055bca8-b29e-48af-8d2f-92838643912b", "AQAAAAIAAYagAAAAEAGMfz/SHGUsRJ59KzS0OBFEKWmYCXR1cCOvkYK9Cmg878qe9THPCOQLZ8gOGn1BrA==", "8e2d83e2-d443-4d06-952e-bb9aeafd8833" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5c24249-b6c4-4195-a678-ba5609933e2c", "AQAAAAIAAYagAAAAEDkHOTLCpiFvNfZMVfXkqAVHeRtdl2t9JYTyngTW1/CP0wSDTgXLBDXYUtNhjgxa6g==", "0a8db3b7-d600-49bd-b9e7-c97008f55f1e" });
        }
    }
}
