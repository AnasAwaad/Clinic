using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionSessionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeen",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConnectionSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisconnectedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionSessions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "IsOnline", "LastSeen", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e52ae181-6abf-4107-9e65-b4f47f8d5574", false, null, "AQAAAAIAAYagAAAAEI78IK89vn6pn4MvcYLONgvWPIi+DjZZNuosj3GvUFCs5u71sVfo+wS4USKfHmbIBg==", "a32dc5bd-7c99-44f0-aa85-083e9f6691e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "IsOnline", "LastSeen", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9651decb-4a5f-4a1c-a5ba-4c00ca8419d5", false, null, "AQAAAAIAAYagAAAAEA+ZxcuXBZ77/gDlWCN0JAliTyuymysNd5IgIfyvQM3XzT5gX9Ki/d3hgmoHG0qTfQ==", "57f40cc6-3cf5-435c-b414-ec60e6055a90" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "IsOnline", "LastSeen", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8f3c837-abb8-424e-b8c7-269853e36ebb", false, null, "AQAAAAIAAYagAAAAEM3PuNDStTAirar/Uz79y8ptAEnpnMXypFLArMH+c17lpNaybfXcyhOTFn+XF7Qjcw==", "09e8959e-e3a4-4c1c-a1c6-2641713a4969" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionSessions");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9063c5b7-4b04-4257-b39f-2c4f16e0dcc9", "AQAAAAIAAYagAAAAEHY+lcm9kZsx2jII0ILtg9P8Ar5bq+nllLEXHHt39DO/A2aZ78bgXxqjk75SSAozMw==", "073f077e-eb46-4509-949b-75ca143b980b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efe64d5c-d9f8-437d-afb7-d49bd38fd4d1", "AQAAAAIAAYagAAAAEHGQZYJLCI7P437nU7LKXmE7cHxsQJB/GZjSmjj8IvTsEL8uttVSWYmaVF2F9pPxYQ==", "b6823b6b-87ef-4e26-912b-e4159f03dee7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96170b75-f69e-42e2-a553-bd02b809382a", "AQAAAAIAAYagAAAAEPu5ZfftUbhx4d2Q4klMh1sM6uOBYFTabZF0jo2FMl5a4eAXZ5Ac2qVu/sPfgMDcSA==", "d7712924-d85a-45bc-86d8-a371841ae2ba" });
        }
    }
}
