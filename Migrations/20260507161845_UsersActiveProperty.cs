using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPolifood.Migrations
{
    /// <inheritdoc />
    public partial class UsersActiveProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "active",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "active",
                table: "Estudiantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "active",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "active",
                table: "Estudiantes");

            migrationBuilder.DropColumn(
                name: "active",
                table: "Admins");
        }
    }
}
