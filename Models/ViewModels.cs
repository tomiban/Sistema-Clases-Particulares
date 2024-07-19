namespace TeddyMVC.Models
{
    public class AlumnoFacturacionViewModel
    {
        public Alumno Alumno { get; set; }
        public IEnumerable<Turno> TurnosPendientes { get; set; }
    }

    public class FacturaViewModel
    {
        public AlumnoViewModel Alumno { get; set; }
        public List<TurnoViewModel> Turnos { get; set; }
        public decimal Total { get; set; }
    }

    public class AlumnoViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Domicilio { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
    }

    public class TurnoViewModel
    {
        public DateTime Fecha { get; set; }
        public int Horas { get; set; }
        public decimal PrecioHora { get; set; }
        public decimal Total => Horas * PrecioHora;
    }
    public class EstadisticasViewModel
    {
        public int TotalHoras { get; set; }
        public decimal TotalFacturado { get; set; }
        public List<AlumnoConHorasClases> HorasClasesPorAlumno { get; set; }
    }

    public class AlumnoConHorasClases
    {
        public int AlumnoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public int HorasClases { get; set; }
    }
}
