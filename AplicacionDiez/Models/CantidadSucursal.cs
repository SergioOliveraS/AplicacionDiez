using System.ComponentModel.DataAnnotations;

namespace AplicacionDiez.Models
{
    public class CantidadSucursal
    {
        public int Id { get; set; }
        [Required]
        public int SucursalId { get; set; }
        public Sucursales? Sucursal { get; set; }
        [Required]
        public int ProductoId { get; set; }
        public Productos? Producto { get; set; }
        [Required]
        public int CantidadProducto { get; set; }


    }
}
