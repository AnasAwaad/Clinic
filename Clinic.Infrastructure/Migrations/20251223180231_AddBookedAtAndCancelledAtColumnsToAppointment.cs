using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookedAtAndCancelledAtColumnsToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookedAt",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledAt",
                table: "Appointments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c139f674-6036-4905-9370-e81b7dff7fec", new DateTime(2025, 12, 23, 20, 2, 26, 669, DateTimeKind.Local).AddTicks(9327), "AQAAAAIAAYagAAAAEFNzPMBuubqqTZx0GnhU4ujASuzuYHBlamT6Q3+Lk/hHUzs06wY2p2I8TzZ10vr9Hg==", "183516d6-20a1-48c8-b3f7-7c94331b83d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f78358af-d47f-482f-a59c-afc887732dbe", new DateTime(2025, 12, 23, 20, 2, 26, 560, DateTimeKind.Local).AddTicks(6281), "AQAAAAIAAYagAAAAEBPNg6UawIRq7BfqJwNji7D1MMVDc824gzXa4Md/+mCApdqzPrLOVh5l78dRHw2vjg==", "4e04a260-11a2-4940-963f-8655b8d1dfba" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "498a5c09-36c9-4c34-80b1-e6865d2f9a95", new DateTime(2025, 12, 23, 20, 2, 26, 462, DateTimeKind.Local).AddTicks(7854), "AQAAAAIAAYagAAAAEKcvxEdvnJui5VGu1vVMKzwaEOQx8Ju90JhwqbosanNZbsERns8hu0+oAMhzMjxDTA==", "26374f2d-98f8-437c-af46-0bf6a09c0cbf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedAt",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CancelledAt",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14ce8c78-3c61-42df-b9f5-57f56c554366", new DateTime(2025, 12, 2, 20, 21, 26, 810, DateTimeKind.Local).AddTicks(8944), "AQAAAAIAAYagAAAAENlGvQXq+xZxkaY/CErn4vrQu26fEcJZrItvbjZ2jORQzi4fa6nHDXoVwMXpLdrGCQ==", "7f9623b9-0a63-45bc-9137-88d58f9c5ea8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a24dc94-c683-4b4e-84b9-be9551cf3252", new DateTime(2025, 12, 2, 20, 21, 26, 700, DateTimeKind.Local).AddTicks(7251), "AQAAAAIAAYagAAAAEHFILaTyR0Na+s+OMisycrbw+toTQWEZu43ZI93+fEBdL9UO0aUVulP5f7zJyMMW5Q==", "faf73ec6-d87d-47f9-ad0e-62ae25a139d8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf1a9a85-24e8-4ec7-984c-8bc6627a3880", new DateTime(2025, 12, 2, 20, 21, 26, 601, DateTimeKind.Local).AddTicks(4101), "AQAAAAIAAYagAAAAEFQq2oUCOFU3jYUtmIJpoAg6FP6nYAn/r10I06+u62c/i1ekrO2FXInGxqut/x/gBA==", "bf50ae97-b3c2-4631-9d62-5c3995b2ec3b" });
        }
    }
}
