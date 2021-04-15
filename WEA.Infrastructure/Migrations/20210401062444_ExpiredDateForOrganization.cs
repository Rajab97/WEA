using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WEA.Infrastructure.Migrations
{
    public partial class ExpiredDateForOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "Organizations",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "Organizations");
        }
    }
}
