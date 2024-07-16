using Microsoft.AspNetCore.Mvc;
using TeddyMVC.Models;
using System;
using System.Linq;
using TeddyMVC.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class AgendaController : Controller
{
    private readonly ApplicationDbContext _context;

    public AgendaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Route("/")]
    [Route("/Agenda")]
    public IActionResult Agenda()
    {
        var items = _context.Turnos
      .Select(t => new
      {
          id = t.Id,
          title = $"{t.Alumno.Nombre.Substring(0, 1)}. {t.Alumno.Apellido}",
          start = t.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
          end = t.Fecha.AddHours(t.Horas).ToString("yyyy-MM-ddTHH:mm:ss"),
          color = "green",
      })
      .ToList();

        ViewBag.Turnos = JsonConvert.SerializeObject(items);
        return View();
    }

}
