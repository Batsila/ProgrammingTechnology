using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class AddDateToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "dbo",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                schema: "dbo",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "dbo",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                schema: "dbo",
                table: "Departments");
        }
    }
}
