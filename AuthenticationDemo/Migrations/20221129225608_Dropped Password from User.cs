using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationDemo.Migrations
{
    public partial class DroppedPasswordfromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "Identity",
                table: "User");

            migrationBuilder.UpdateData(
                schema: "Identity",
                table: "User",
                keyColumn: "FacebookID",
                keyValue: null,
                column: "FacebookID",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FacebookID",
                schema: "Identity",
                table: "User",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FacebookID",
                schema: "Identity",
                table: "User",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "Identity",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
