using Microsoft.EntityFrameworkCore.Migrations;

namespace Referendum.core.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "Password" },
                values: new object[] { 1, "Garegin", "Ayvazyan", "Garegin", "Sa123456!" });

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "Password" },
                values: new object[] { 2, "Yelena", "Ayvazyan", "Yelena", "Sa123456" });

            migrationBuilder.InsertData(
                table: "UserDb",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "Password" },
                values: new object[] { 3, "Meline", "Davtyan", "Meline", "Sa123456" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserDb",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
