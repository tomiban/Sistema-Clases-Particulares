using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeddyMVC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelALumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Alumnos",
                newName: "Telefono");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Telefono",
                table: "Alumnos",
                newName: "Email");
        }
    }
}
