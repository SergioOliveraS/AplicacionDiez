using Microsoft.EntityFrameworkCore;

namespace AplicacionDiez.Models
{
    public class ACMEContext: DbContext
    {
        public ACMEContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<Productos> Productos { get; set; } 
        public DbSet<CantidadSucursal> CantidadSucursales { get; set; }
    }
}
