using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AplicacionDiez.Models
{
    public class Productos
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="Nombre muy largo")]
        public string Nombre { get; set; }
        [Required]
        [Column(TypeName ="decimal(6,2)")]
        public decimal PrecioUnitario { get; set; }
        [Required]
        [StringLength(12, MinimumLength =12, ErrorMessage ="Código de barras no válido")]
        public string CodigoBarras { get; set; }
        [StringLength(120, ErrorMessage ="Texto muy largo")]
        public string Detalles { get; set; }
    }
}
