using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class ChangeSalaryLogics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                schema: "dbo",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_EmployeeId",
                schema: "dbo",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "dbo",
                table: "Salaries");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryInfoId",
                schema: "dbo",
                table: "Employees",
                column: "SalaryInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Salaries_SalaryInfoId",
                schema: "dbo",
                table: "Employees",
                column: "SalaryInfoId",
                principalSchema: "dbo",
                principalTable: "Salaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Salaries_SalaryInfoId",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SalaryInfoId",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                schema: "dbo",
                table: "Salaries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                schema: "dbo",
                table: "Salaries",
                column: "EmployeeId",
                unique: true);

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
    }
}
