namespace TeddyMVC.Models
{
    public class AlumnoFacturacionViewModel
    {
        public Alumno Alumno { get; set; }
        public IEnumerable<Turno> TurnosPendientes { get; set; }
    }

    public class FacturaViewModel
    {
        public Alumno Alumno { get; set; }
        public List<Turno> Turnos { get; set; }
        public decimal Total { get; set; }
    }
}
