using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace AplicacionDiez.Models
{
    public class Sucursales
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage ="Nombre no aceptado")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(120, MinimumLength =10, ErrorMessage ="Ingresa una ubicación más larga")]
        public string Ubicacion { get; set; }
    }
}
