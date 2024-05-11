using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class FirstFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ClaimValue",
                table: "Claim",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ClaimValue",
                table: "Claim",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
