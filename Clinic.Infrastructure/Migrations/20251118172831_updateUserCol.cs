using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "402ddff0-09e1-425f-b41b-2fc1ec5668b0",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0d223c2-c36a-4103-b581-d478cea63329", false, "AQAAAAIAAYagAAAAEHxjYctCLPPBbZXeGp7/lWv8mVJ8yJ0A74lXDWldpsiZG3Lm8B88pEI2+L2C2apCYg==", "bc224d0a-84ea-4904-be90-c22c58cba5a8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "556c1c99-2d3a-4988-a80a-46ab2f14ea71",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "446237fd-8491-476b-a8fa-8a4de5c95781", false, "AQAAAAIAAYagAAAAEPwCXNPA09FPh1CHo1F16PfVlPYTtYZisYPU5Uku12CHm8YGFiV96EhdYiJ4IMzCkQ==", "76dc4133-9f6e-4d24-bca3-1c7382478fbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "57ff9f9a-6b56-4c6b-beeb-62cf2c6fd66e",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38be2e82-cb37-417b-9d22-208fc6ef1baa", false, "AQAAAAIAAYagAAAAEA8dhh7Ws7KCV4I/BFcmeeLfrIbVMi8aQOqXWP93DNDEMVomXQalePozMkAe2Io0yQ==", "c29d894b-dda3-4831-8119-e7118a59986b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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
    }
}
