using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeddyMVC.Data;
using TeddyMVC.Models;

namespace TeddyMVC.Controllers
{
    [Route("[controller]")]
    public class EstadisticasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EstadisticasController> _logger;

        public EstadisticasController(ILogger<EstadisticasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener la fecha actual y el rango del mes actual
            var currentDate = DateTime.Now;
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));

            // Consulta para obtener los turnos del mes actual incluyendo los datos de los alumnos
            var turnosDelMes = await _context.Turnos
                .Where(t => t.Fecha >= startOfMonth && t.Fecha <= endOfMonth)
                .Include(t => t.Alumno)
                .ToListAsync();

            // Calcular el total de horas de clases y el total facturado
            var totalHoras = turnosDelMes.Sum(t => t.Horas);
            var totalFacturado = turnosDelMes.Sum(t => t.Horas * t.PrecioHora);

            // Obtener la lista de alumnos y las horas de clases por cada alumno
            var alumnosConHoras = turnosDelMes
                .GroupBy(t => t.Alumno)
                .Select(g => new AlumnoConHorasClases
                {
                    AlumnoId = g.Key.Id,
                    Nombre = g.Key.Nombre,
                    Apellido = g.Key.Apellido,
                    DNI = g.Key.DNI,
                    HorasClases = g.Sum(t => t.Horas)
                })
                .ToList();

            // Crear el modelo de vista para pasar a la vista
            var viewModel = new EstadisticasViewModel
            {
                TotalHoras = totalHoras,
                TotalFacturado = totalFacturado,
                HorasClasesPorAlumno = alumnosConHoras
            };

            return View(viewModel);
        }
    }
}