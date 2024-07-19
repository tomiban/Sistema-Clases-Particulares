
namespace TeddyMVC.Models;
using System.ComponentModel.DataAnnotations;
public class Alumno
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Apellido { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string DNI { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Domicilio { get; set; } = string.Empty;
    public string? Ciudad { get; set; }  = string.Empty;
    public string? Telefono { get; set; }  = string.Empty; 
    public ISet<Turno> Turnos { get; } = new HashSet<Turno>();
    public virtual ICollection<Historial> Historial { get; set; } = new List<Historial>();
}
