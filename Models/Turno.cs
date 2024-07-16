namespace TeddyMVC.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Turno
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("AlumnoId")]
    public int AlumnoId { get; set; }

    public virtual Alumno? Alumno { get; set; }

    [Required]
    public DateTime Fecha { get; set; }

    [Required]
    public int Horas { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecioHora { get; set; }

    public string? Asignatura { get; set; }

    [Required]
    public bool Pagado { get; set; }

    // Navigation property
    public virtual ICollection<Historial> Historiales { get; set; } = new List<Historial>();

}
