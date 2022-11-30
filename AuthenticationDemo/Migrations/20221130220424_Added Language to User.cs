using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationDemo.Migrations
{
    public partial class AddedLanguagetoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoogleID",
                schema: "Identity",
                table: "User",
                newName: "GoogleId");

            migrationBuilder.RenameColumn(
                name: "FacebookID",
                schema: "Identity",
                table: "User",
                newName: "FacebookId");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageId",
                schema: "Identity",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "GoogleId",
                schema: "Identity",
                table: "User",
                newName: "GoogleID");

            migrationBuilder.RenameColumn(
                name: "FacebookId",
                schema: "Identity",
                table: "User",
                newName: "FacebookID");
        }
    }
}
