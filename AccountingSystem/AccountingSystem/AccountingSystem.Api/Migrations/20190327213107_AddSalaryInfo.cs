using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class AddSalaryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "dbo",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryInfoId",
                schema: "dbo",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SalaryInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    Salary = table.Column<double>(nullable: false),
                    BankAccount = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryInfo_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "dbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryInfo_EmployeeId",
                table: "SalaryInfo",
                column: "EmployeeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryInfo");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryInfoId",
                schema: "dbo",
                table: "Employees");
        }
    }
}
