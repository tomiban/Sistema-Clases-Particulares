namespace TeddyMVC.Models
{
    public class AlumnoFacturacionViewModel
    {
        public Alumno Alumno { get; set; }
        public IEnumerable<Turno> TurnosPendientes { get; set; }
    }

    public class FacturaViewModel
    {  public AlumnoViewModel Alumno { get; set; }
        public List<TurnoViewModel> Turnos { get; set; }
        public decimal Total { get; set; }
    }

    public class AlumnoViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Domicilio { get; set; }
    }

    public class TurnoViewModel
    {
        public DateTime Fecha { get; set; }
        public int Horas { get; set; }
        public decimal PrecioHora { get; set; }
        public decimal Total => Horas * PrecioHora;
    }

}
