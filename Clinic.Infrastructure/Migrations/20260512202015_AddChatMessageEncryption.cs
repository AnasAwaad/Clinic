using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChatMessageEncryption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Messages");

            migrationBuilder.AddColumn<byte[]>(
                name: "EncryptedAesKey",
                table: "Messages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "EncryptedMessage",
                table: "Messages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Iv",
                table: "Messages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ProtectedRsaPrivateKeyPem",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RsaPublicKeyPem",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProtectedRsaPrivateKeyPem", "RsaPublicKeyPem", "SecurityStamp" },
                values: new object[] { "1f82d916-623f-4f53-a40c-0d2ecb8447a6", new DateTime(2026, 5, 12, 23, 20, 7, 473, DateTimeKind.Local).AddTicks(7137), "AQAAAAIAAYagAAAAEKgS4VedRW6LwNeMlAXh64R0HuN/LQPRWAViBfUhw3QjEZ5jTvKCCyKzYERIFNVHoQ==", null, null, "b02c762f-2312-402d-8931-ce001e2b50c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProtectedRsaPrivateKeyPem", "RsaPublicKeyPem", "SecurityStamp" },
                values: new object[] { "e7d372de-985a-4336-afaf-73f586ea326e", new DateTime(2026, 5, 12, 23, 20, 7, 259, DateTimeKind.Local).AddTicks(260), "AQAAAAIAAYagAAAAEJJ+hKu+SozJlb5gcZ+J0vpkf14OfDjCqAKNirfV0w50FKk/mr9B6x4uFay1fj7+xg==", null, null, "54bee7f7-1f55-4f49-8324-c9c1eb868adc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "ProtectedRsaPrivateKeyPem", "RsaPublicKeyPem", "SecurityStamp" },
                values: new object[] { "5b08f57c-1a8f-4717-b3fd-b940da767f8c", new DateTime(2026, 5, 12, 23, 20, 7, 24, DateTimeKind.Local).AddTicks(8940), "AQAAAAIAAYagAAAAEJ81KGMDSL8zGqHC98uuzhQc4slpAadAQHDhyg8ZaQrX6ZjNLsG+B7EyPAQ6OZmdtQ==", null, null, "9fc09a37-1887-41d7-a85d-8f7cc08352f7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedAesKey",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "EncryptedMessage",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Iv",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProtectedRsaPrivateKeyPem",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RsaPublicKeyPem",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
