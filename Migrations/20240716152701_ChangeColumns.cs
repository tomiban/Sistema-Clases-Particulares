using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeddyMVC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "Historial");

            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "Turnos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pagado",
                table: "Turnos");

            migrationBuilder.AddColumn<bool>(
                name: "Pagado",
                table: "Historial",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
