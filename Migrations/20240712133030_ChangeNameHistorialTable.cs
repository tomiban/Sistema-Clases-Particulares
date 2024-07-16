using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeddyMVC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameHistorialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historiales_Alumnos_AlumnoId",
                table: "Historiales");

            migrationBuilder.DropForeignKey(
                name: "FK_Historiales_Turnos_TurnoId",
                table: "Historiales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales");

            migrationBuilder.RenameTable(
                name: "Historiales",
                newName: "Historial");

            migrationBuilder.RenameIndex(
                name: "IX_Historiales_TurnoId",
                table: "Historial",
                newName: "IX_Historial_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Historiales_AlumnoId",
                table: "Historial",
                newName: "IX_Historial_AlumnoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historial",
                table: "Historial",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Historial_Alumnos_AlumnoId",
                table: "Historial",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historial_Turnos_TurnoId",
                table: "Historial",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historial_Alumnos_AlumnoId",
                table: "Historial");

            migrationBuilder.DropForeignKey(
                name: "FK_Historial_Turnos_TurnoId",
                table: "Historial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historial",
                table: "Historial");

            migrationBuilder.RenameTable(
                name: "Historial",
                newName: "Historiales");

            migrationBuilder.RenameIndex(
                name: "IX_Historial_TurnoId",
                table: "Historiales",
                newName: "IX_Historiales_TurnoId");

            migrationBuilder.RenameIndex(
                name: "IX_Historial_AlumnoId",
                table: "Historiales",
                newName: "IX_Historiales_AlumnoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historiales",
                table: "Historiales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Historiales_Alumnos_AlumnoId",
                table: "Historiales",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Historiales_Turnos_TurnoId",
                table: "Historiales",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
