using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class AddTimeCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryInfo_Employees_EmployeeId",
                table: "SalaryInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryInfo",
                table: "SalaryInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "SalaryInfo",
                newName: "Salaries",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_SalaryInfo_EmployeeId",
                schema: "dbo",
                table: "Salaries",
                newName: "IX_Salaries_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Departments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salaries",
                schema: "dbo",
                table: "Salaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                schema: "dbo",
                table: "Departments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TimeCard",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    Time = table.Column<double>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeCard_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "dbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeCard_EmployeeId",
                table: "TimeCard",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                schema: "dbo",
                table: "Employees",
                column: "DepartmentId",
                principalSchema: "dbo",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                schema: "dbo",
                table: "Salaries",
                column: "EmployeeId",
                principalSchema: "dbo",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                schema: "dbo",
                table: "Salaries");

            migrationBuilder.DropTable(
                name: "TimeCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salaries",
                schema: "dbo",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                schema: "dbo",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Salaries",
                schema: "dbo",
                newName: "SalaryInfo");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "dbo",
                newName: "Department");

            migrationBuilder.RenameIndex(
                name: "IX_Salaries_EmployeeId",
                table: "SalaryInfo",
                newName: "IX_SalaryInfo_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryInfo",
                table: "SalaryInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryInfo_Employees_EmployeeId",
                table: "SalaryInfo",
                column: "EmployeeId",
                principalSchema: "dbo",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Department_DepartmentId",
                schema: "dbo",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
