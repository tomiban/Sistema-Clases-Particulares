using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeddyMVC.Models;

namespace TeddyMVC.Utils
{
    public static class ViewModelMapper
    {
        public static AlumnoViewModel MapToAlumnoViewModel(Alumno alumno)
        {
            return new AlumnoViewModel
            {
                Nombre = alumno.Nombre,
                Apellido = alumno.Apellido,
                DNI = alumno.DNI,
                Domicilio = alumno.Domicilio,
                Ciudad = alumno.Ciudad,
                Telefono = alumno.Telefono
            };
        }

        public static TurnoViewModel MapToTurnoViewModel(Turno turno)
        {
            return new TurnoViewModel
            {
                Fecha = turno.Fecha,
                Horas = turno.Horas,
                PrecioHora = turno.PrecioHora
            };
        }

        public static FacturaViewModel MapToFacturaViewModel(Alumno alumno, List<Turno> turnos)
        {
            var alumnoViewModel = MapToAlumnoViewModel(alumno);
            var listaTurnos = turnos.Select(MapToTurnoViewModel).ToList();

            return new FacturaViewModel
            {
                Alumno = alumnoViewModel,
                Turnos = listaTurnos,
                Total = listaTurnos.Sum(t => t.Total)
            };
        }
    }
}


