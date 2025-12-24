using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fcc020c5-a677-440f-8aa0-6410f7a873cd", new DateTime(2025, 12, 24, 13, 33, 9, 234, DateTimeKind.Local).AddTicks(5375), "AQAAAAIAAYagAAAAEFpPAvKrAiT+Yh+jx7ATzVIBPsp9fNiaVmQ44ym2YkkTJDOkmGiIhrczwz5Vu3VKxQ==", "a1672f21-1298-4e33-ab1a-e9bef801a994" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "615d1edd-cb34-4395-9bd4-2969ad1ddaec", new DateTime(2025, 12, 24, 13, 33, 9, 143, DateTimeKind.Local).AddTicks(6856), "AQAAAAIAAYagAAAAEN7vTBIWD3iU2KQzRxnnQMobZyANLr5zEBeTUs7I6mYENGqZMKvZnBwuqzPzLOVHeg==", "260fd220-158c-49fb-805b-5f425c7d38e2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52661455-ef46-480e-beb1-2f6e4ac29bb8", new DateTime(2025, 12, 24, 13, 33, 9, 47, DateTimeKind.Local).AddTicks(3259), "AQAAAAIAAYagAAAAEHXpJTsvOwmxyAIoDE26+QdmnwEH2F/MFnl+bPDBnX4JoiTN+JPqJCEUtb+Qs/9eDQ==", "c3723b6f-2613-4f77-8cf0-adc921c09693" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

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
    }
}
