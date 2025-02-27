using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

public class OpinionesClientes
{
    [Key]
    public int OpinionID { get; set; }

    [Required(ErrorMessage = "El Cliente es obligatorio.")]
    [Display(Name = "Cliente")]
    public int Codigo { get; set; }

    [Required(ErrorMessage = "El Producto es obligatorio.")]
    [Display(Name = "Producto")]
    public int? ProductoID { get; set; }

    [Required(ErrorMessage = "La Calificación es obligatoria.")]
    [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
    public int Calificacion { get; set; }

    [StringLength(500)]
    public string Comentario { get; set; }

    [Required(ErrorMessage = "La Fecha es obligatoria.")]
    public DateTime Fecha { get; set; } = DateTime.Now;
}
