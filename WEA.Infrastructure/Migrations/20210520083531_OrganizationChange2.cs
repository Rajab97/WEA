using Microsoft.EntityFrameworkCore.Migrations;

namespace WEA.Infrastructure.Migrations
{
    public partial class OrganizationChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lattitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Organizations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Lattitude",
                table: "Organizations",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longtitude",
                table: "Organizations",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
