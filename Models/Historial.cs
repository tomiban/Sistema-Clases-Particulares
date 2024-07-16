namespace TeddyMVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Historial
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("AlumnoId")]
    public int AlumnoId { get; set; }
    
    public virtual Alumno Alumno { get; set; } = null!;
    
    [Required]
    public int TurnoId { get; set; }
    
    [ForeignKey("TurnoId")]
    public virtual Turno Turno { get; set; } = null!;
    

}
