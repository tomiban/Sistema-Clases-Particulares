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
namespace TeddyMVC.Controllers
{
    public class AlumnosController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<AlumnosController> _logger;
        public AlumnosController(ApplicationDbContext context, ILogger<AlumnosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // This method retrieves a list of all students (alumnos) from the database,
        // along with information about whether each student has an outstanding debt.
        // It uses Entity Framework Core to make an asynchronous query to the database.
        // The query includes the list of lessons (turnos) for each student,
        // which allows us to calculate the total amount owed by each student.
        // The result is returned asynchronously as an ActionResult, which is a special
        // type used by ASP.NET Core to return data to the client.
        public async Task<ActionResult> Index()
        {
            // Retrieve all students and their lessons from the database.
            var alumnos = await _context.Alumnos
               .Include(a => a.Turnos)
               .ToListAsync();

            // Get a list of all student IDs who have unpaid lessons.
            var alumnosConDeuda = _context.Turnos
                .Where(t => t.Pagado == false)
                .Select(t => t.AlumnoId)
                .Distinct()
                .ToHashSet();

            // For each student, create a new anonymous object that contains
            // the student data and a boolean indicating whether they have a debt.
            var model = alumnos.Select(a => new
            {
                // The student data.
                Alumno = a,
                // True if the student has unpaid lessons, false otherwise.
                TieneDeuda = alumnosConDeuda.Contains(a.Id)
            });

            // Return the list of student data asynchronously.
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error en {state.Key}: {error.ErrorMessage}");
                    }
                }
            }
            else
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Alumno creado";
                return RedirectToAction("Index");
            }
            return View(alumno);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Update(alumno);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Alumno actualizado";
                return RedirectToAction("Index");
            }
            return View(alumno);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Alumno eliminado exitosamente!";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Facturacion(int id)
        {
            var alumno = await _context.Alumnos
                .Where(a => a.Id == id)
                .Include(a => a.Turnos)
                .FirstOrDefaultAsync();

            if (alumno == null)
            {
                return NotFound();
            }

            var turnosPendientes = alumno.Turnos
                .Where(t => t.Pagado == false)
                .ToList();

            return View(new AlumnoFacturacionViewModel
            {
                Alumno = alumno,
                TurnosPendientes = turnosPendientes
            });
        }
    }
}
