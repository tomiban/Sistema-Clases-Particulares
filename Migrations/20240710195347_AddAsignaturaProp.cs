using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeddyMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddAsignaturaProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Asignatura",
                table: "Turnos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Asignatura",
                table: "Turnos");
        }
    }
}
