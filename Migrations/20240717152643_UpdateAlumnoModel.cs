using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeddyMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAlumnoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Alumnos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Alumnos",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Alumnos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Alumnos");
        }
    }
}
