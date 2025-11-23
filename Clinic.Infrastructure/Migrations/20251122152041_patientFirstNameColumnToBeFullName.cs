using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class patientFirstNameColumnToBeFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c09c65a6-c614-40ff-829c-2b1906ba3623", "Secretary", "AQAAAAIAAYagAAAAEJGbPmnmWzUvRfiILXso4AtPAreyoPmkYTUrLtrLNjCtExfr9iU/qsl+KNFsZCOVTA==", "6e60d5ac-b948-46d0-996c-add131d3cf7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee6326de-4810-43dc-b3b6-61dd6ab45003", "Admin", "AQAAAAIAAYagAAAAEE44tRnKKGcybm5h/KwLsubRGu0B133WdA3FThxhOM1ZJG9xF/CRWAybZHqutDnfrw==", "9ef15e20-f92b-4df6-b9fe-50c71182f6b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "FullName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4752f689-d75e-4af0-9b18-ee6a7ce6704d", "Doctor", "AQAAAAIAAYagAAAAEDoeNl47xdESzrdLpS+8Ac5CFTRznWIkodWeGGuXIMC2zVFtV7r7SyVAajaHluxg6A==", "dcca8150-e8f5-4899-86cf-463b09d0c066" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d119db65-61a0-4889-80c2-122380d242f5", "Secretary", "", "AQAAAAIAAYagAAAAENFzVe5Jx3Z4GvS6fd97kJgpdoHqpXpNEGX7Q78cvYqyUeySv29FZN5UW15EJok4mg==", "28975f71-d999-476b-8539-73eb6a83ce8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4ca7032d-89ca-4cc7-971d-4d7eba94582a", "Admin", "", "AQAAAAIAAYagAAAAEPGKdJygp1/ulL6yAIRoRYYsDEc/w3aHZyQks9F5+XogcRwMVpRGpnOeZJkPwUpDiw==", "b0502459-10cd-4691-b27e-56e8d6132e8c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5b0321e-173a-409e-9e7f-0c4f143bff77", "Doctor", "", "AQAAAAIAAYagAAAAEPLN66diRDPcuJkS0TlaJri7O2FdRu1w3sChO8O/TWEsFPBmrPlZoTOiRc1duxtICg==", "864dabe8-9cc5-4061-b0a9-7bfde07369bd" });
        }
    }
}
