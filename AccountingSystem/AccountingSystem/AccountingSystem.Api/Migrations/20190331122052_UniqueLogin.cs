using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountingSystem.Api.Migrations
{
    public partial class UniqueLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeCard_Employees_EmployeeId",
                table: "TimeCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeCard",
                table: "TimeCard");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "TimeCard",
                newName: "TimeCards",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_TimeCard_EmployeeId",
                schema: "dbo",
                table: "TimeCards",
                newName: "IX_TimeCards_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                schema: "dbo",
                table: "Users",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "dbo",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                schema: "dbo",
                table: "Users",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                schema: "dbo",
                table: "TimeCards",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeCards",
                schema: "dbo",
                table: "TimeCards",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeCards_Employees_EmployeeId",
                schema: "dbo",
                table: "TimeCards",
                column: "EmployeeId",
                principalSchema: "dbo",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeCards_Employees_EmployeeId",
                schema: "dbo",
                table: "TimeCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeCards",
                schema: "dbo",
                table: "TimeCards");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "dbo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "TimeCards",
                schema: "dbo",
                newName: "TimeCard");

            migrationBuilder.RenameIndex(
                name: "IX_TimeCards_EmployeeId",
                table: "TimeCard",
                newName: "IX_TimeCard_EmployeeId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "TimeCard",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeCard",
                table: "TimeCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeCard_Employees_EmployeeId",
                table: "TimeCard",
                column: "EmployeeId",
                principalSchema: "dbo",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
