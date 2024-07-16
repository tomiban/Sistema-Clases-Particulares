using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeddyMVC.Models;

namespace TeddyMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Historial> Historial { get; set; }
    }
}