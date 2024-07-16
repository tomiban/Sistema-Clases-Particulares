using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TeddyMVC.Data;

namespace TeddyMVC.Controllers
{
    public class HistorialController : Controller
    {
        private readonly ILogger<HistorialController> _logger;
        private readonly ApplicationDbContext _context;

        public HistorialController(ILogger<HistorialController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var historial = await _context.Historial
        .Include(h => h.Alumno)
        .Include(h => h.Turno)
        .ToListAsync();
            return View(historial);
        }
    }
}