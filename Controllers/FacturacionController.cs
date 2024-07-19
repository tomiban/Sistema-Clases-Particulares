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
using Twilio;
using Twilio.Rest.Api.V2010.Account;

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

            string fileName = $"Clases_{alumno.Nombre}{alumno.Apellido}_{DateTime.Now:dd-MM-yyyy}.pdf";


            var pdf = new ViewAsPdf("_FacturaPartial", facturaViewModel)
            {
                FileName = fileName,
                PageMargins = { Top = 10, Bottom = 10 },
                CustomSwitches = "--disable-smart-shrinking" // Añade cualquier otro parámetro necesario
            };

            return pdf;
        }

        /*  public async Task<IActionResult> SendFacturaByWhatsapp(int[] turnosIds)
         {
             TwilioClient.Init("AC5da2a73cfbfa1e972bf20f674a186106", "6cce08c5ee67897bd86eee99daebe666");

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



             string fileName = $"Clases_{alumno.Nombre}{alumno.Apellido}_{DateTime.Now:dd-MM-yyyy}.pdf";

             // Genera el PDF
             var pdf = new ViewAsPdf("_FacturaPartial", facturaViewModel)
             {
                 PageMargins = { Top = 10, Bottom = 10 },
                 CustomSwitches = "--disable-smart-shrinking" // Añade cualquier otro parámetro necesario
             };

             var pdfBytes = await pdf.BuildFile(this.ControllerContext);

             // Guarda el PDF temporalmente
             var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
             await System.IO.File.WriteAllBytesAsync(tempFilePath, pdfBytes);

             // Convierte el archivo temporal en una URL de archivo
             var fileUri = new Uri($"file://{tempFilePath}");
             var mediaUrl = new List<Uri> { fileUri };

             // Enviar el PDF por WhatsApp usando Twilio
             var message = MessageResource.Create(
                 body: "Aquí está tu factura.",
                 from: new Twilio.Types.PhoneNumber("whatsapp:+14784296126"), // Número de Twilio para WhatsApp
                 to: new Twilio.Types.PhoneNumber($"whatsapp:{alumno.Telefono}"), // Número de teléfono del alumno en formato internacional
                 mediaUrl: mediaUrl
             );

             // Limpia el archivo temporal después de enviarlo
             System.IO.File.Delete(tempFilePath);

             return Ok("Factura enviada por WhatsApp.");
         } */

    }
}


