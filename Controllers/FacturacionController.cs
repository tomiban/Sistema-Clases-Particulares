using System.Linq;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TeddyMVC.Data;
using TeddyMVC.Interfaces;
using TeddyMVC.Utils;
using TeddyMVC.Models;
using Rotativa.AspNetCore;

namespace TeddyMVC.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConverter _converter;
        private readonly IViewRenderService _viewRenderService;

        public FacturacionController(ApplicationDbContext context, IConverter converter, IViewRenderService viewRenderService)
        {
            _context = context;
            _converter = converter;
            _viewRenderService = viewRenderService;
        }



        public async Task<IActionResult> GenerarFacturaView(int[] turnosIds)
        {
            if (turnosIds == null || turnosIds.Length == 0)
            {
                return BadRequest("No se han seleccionado turnos para facturar.");
            }

            var turnos = await _context.Turnos
                .Include(t => t.Alumno)
                .Where(t => turnosIds.Contains(t.Id))
                .ToListAsync();

            if (turnos == null || !turnos.Any())
            {
                return NotFound("No se encontraron turnos para los IDs proporcionados.");
            }

            Alumno alumno = turnos.First().Alumno;
            var facturaViewModel = ViewModelMapper.MapToFacturaViewModel(alumno, turnos);

            ViewBag.TurnosIds = turnosIds;

            return View("Factura", facturaViewModel);
        }


        public async Task<IActionResult> DownloadFacturaPdf(int[] turnosIds)
        {
            if (turnosIds == null || turnosIds.Length == 0)
            {
                return BadRequest("No se han seleccionado turnos para facturar.");
            }

            var turnos = await _context.Turnos
                .Include(t => t.Alumno)
                .Where(t => turnosIds.Contains(t.Id))
                .ToListAsync();

            if (turnos == null || !turnos.Any())
            {
                return NotFound("No se encontraron turnos para los IDs proporcionados.");
            }

            Alumno alumno = turnos.First().Alumno;

            var facturaViewModel = ViewModelMapper.MapToFacturaViewModel(alumno, turnos);

            var pdf = new ViewAsPdf("_FacturaPartial", facturaViewModel)
            {
                FileName = "Factura.pdf",
                PageMargins = { Top = 10, Bottom = 10 },
                CustomSwitches = "--disable-smart-shrinking" // Añade cualquier otro parámetro necesario
            };

            return pdf;
        }


    }
}


