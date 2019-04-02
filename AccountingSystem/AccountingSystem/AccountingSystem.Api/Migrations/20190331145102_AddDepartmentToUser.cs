using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class AddDepartmentToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                schema: "dbo",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                schema: "dbo",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                schema: "dbo",
                table: "Users",
                column: "DepartmentId",
                principalSchema: "dbo",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "dbo",
                table: "Users");
        }
    }
}
