using Microsoft.EntityFrameworkCore.Migrations;

namespace WEA.Infrastructure.Migrations
{
    public partial class OrganizationDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                table: "Organizations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Organizations",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Lattitude",
                table: "Organizations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longtitude",
                table: "Organizations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationAddress",
                table: "Organizations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Organizations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelephoneNumber",
                table: "Organizations",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_IdentificationNumber",
                table: "Organizations",
                column: "IdentificationNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_IdentificationNumber",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Lattitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationAddress",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "TelephoneNumber",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationName",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
