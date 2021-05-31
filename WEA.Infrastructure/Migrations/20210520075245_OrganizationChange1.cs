using Microsoft.EntityFrameworkCore.Migrations;

namespace WEA.Infrastructure.Migrations
{
    public partial class OrganizationChange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Organizations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultRole",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Organizations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
