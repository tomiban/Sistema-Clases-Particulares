using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeddyMVC.Data;
using TeddyMVC.Models;

/* 
    TODO -> Funcion para abonar un turno (historial) en la vista de Turnos
    TODO -> Implementar logica y vista para ver historial completo de turnos  (puede ser una vista aparte o un filtro en la tabla de turnos)
 */

namespace TeddyMVC.Controllers
{
    public class TurnosController : Controller
    {
        private readonly ILogger<TurnosController> _logger;
        private readonly ApplicationDbContext _context;

        public TurnosController(ApplicationDbContext context, ILogger<TurnosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var turnos = await _context.Turnos
      .Include(t => t.Alumno) // Incluye la propiedad de navegación Alumno
      .Where(t => t.Fecha >= DateTime.Now && t.Fecha < DateTime.Now.AddDays(1) && t.Pagado == false)
      .ToListAsync();
            return View(turnos);
        }

        public IActionResult Create()
        {
            ViewBag.Alumnos = new SelectList(_context.Alumnos.Select(a => new
            {
                a.Id,
                NombreCompleto = a.Nombre + " " + a.Apellido
            }).ToList(), "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Turno turno)
        {
            if (!ModelState.IsValid)
            {
                return View(turno);
            }

            await _context.AddAsync(turno);
            await _context.SaveChangesAsync();

            var nuevoHistorial = new Historial
            {
                TurnoId = turno.Id,
                AlumnoId = turno.AlumnoId,
            };

            await _context.AddAsync(nuevoHistorial);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }
            ViewBag.Alumnos = new SelectList(_context.Alumnos, "Id", "Nombre");
            return View(turno);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Turno turno)
        {
            if (ModelState.IsValid)
            {
                var turnoEncontrado = await _context.Turnos.FindAsync(turno.Id);
                if (turnoEncontrado == null)
                {
                    return NotFound();
                }

                _context.Entry(turnoEncontrado).CurrentValues.SetValues(turno);

                var nuevoHistorial = new Historial
                {
                    TurnoId = turno.Id,
                    AlumnoId = turno.AlumnoId,

                };
                await _context.AddAsync(nuevoHistorial);

                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Turno editado";
                return RedirectToAction(nameof(Index), new { area = "" });
            }
            ViewBag.Alumnos = new SelectList(_context.Alumnos, "Id", "Nombre");
            return View(turno);
        }

        [HttpPost]
        public async Task<JsonResult> EditEventDate(int id, DateTime start, DateTime? end)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return Json(new { success = false });
            }

            turno.Fecha = start;
            if (end.HasValue)
            {
                turno.Fecha = end.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Turno actualizado!";
                return Json(new { success = true });
            }
            catch (Exception)
            {
                TempData["AlertMessage"] = "No se pudo actualizar!";
                return Json(new { success = false });
            }
        }

        public async Task<IActionResult> MarcarPagado(int? id)
        {
            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var evento = await _context.Turnos.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Pagado = true;

            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Turno marcado como pagado exitosamente!";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.Turnos == null)
            {
                return NotFound();
            }

            var evento = await _context.Turnos.FirstOrDefaultAsync(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            _context.Turnos.Remove(evento);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Evento eliminado exitosamente!!!";
            return RedirectToAction("Index");
        }
    }
}
