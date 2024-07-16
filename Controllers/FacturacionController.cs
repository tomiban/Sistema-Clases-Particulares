using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeddyMVC.Data;
using TeddyMVC.Models;

/* 
  TODO -> Implementar funciones para visualizar la facturación (turnos sin pagar) del alumno
 */

namespace TeddyMVC.Controllers
{
    public class FacturacionController : Controller
    {

        private readonly ILogger<FacturacionController> _logger;
        private readonly ApplicationDbContext _context;

        public FacturacionController(ILogger<FacturacionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerarFactura(int[] turnosIds)
        {
            if (turnosIds == null || turnosIds.Length == 0)
            {
                return BadRequest("No se han seleccionado turnos para facturar.");
            }

            // Obtener los turnos seleccionados
            var turnos = await _context.Turnos
                .Include(t => t.Alumno)
                .Where(t => turnosIds.Contains(t.Id))
                .ToListAsync();

            if (turnos == null || !turnos.Any())
            {
                return NotFound("No se encontraron turnos para los IDs proporcionados.");
            }

            // Crear la factura (puedes personalizar esta parte según tus necesidades)
            var facturaViewModel = new FacturaViewModel
            {
                Alumno = turnos.First().Alumno,
                Turnos = turnos,
                Total = turnos.Sum(t => t.Horas * t.PrecioHora)
            };

            // Puedes guardar la factura en la base de datos si es necesario aquí

            return View("Factura", facturaViewModel); // Asegúrate de tener una vista Factura.cshtml para mostrar la factura
        }
    }
}
