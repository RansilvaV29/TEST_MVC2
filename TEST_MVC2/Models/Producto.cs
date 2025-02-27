
using System.ComponentModel.DataAnnotations;

namespace TEST_MVC2.Models
{
    public class Producto
    {
        [Key]
        public int ProductoID { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }
    }
}
