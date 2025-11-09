using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMessagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4943e983-d0db-4cbe-9f46-aa90ffd11131", "AQAAAAIAAYagAAAAEJuPEEdbe9MeoehZqGqOhDPsSD0PnS7GmfbKlJXu0o193Z2KurC6sd+/ea7gNF02Hg==", "f325e0b1-8ee9-4830-9d89-a0004bd697d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b6c6b17-9a0c-4db4-aef5-a52b0c1d9bc9", "AQAAAAIAAYagAAAAEBRud8ku1I8VQRPxC64P60SMiEcn94ZfOm03yL5xPSQeNvlAuASXwLiJ2fGydbK9jw==", "19116bd0-f035-4e7d-8a7a-901affa3c8ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "daec1842-cbed-4e9a-aa8f-37c9303ddc20", "AQAAAAIAAYagAAAAEKtY0mrqOWW23U4ob2Whlc2S5W/eTUgjWeTbTjulAvyhrWCAa6cEmJrsFvRj8d+FbA==", "ee61ddab-9e75-4025-bc2b-1e18b3c41acb" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

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
    }
}
