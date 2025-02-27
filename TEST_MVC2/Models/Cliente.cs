using System.ComponentModel.DataAnnotations;

namespace TEST_MVC2.Models
{
    public class Cliente
    {
        [Required]
        public int Codigo { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public Boolean Estado { get; set; } 
    }
}
