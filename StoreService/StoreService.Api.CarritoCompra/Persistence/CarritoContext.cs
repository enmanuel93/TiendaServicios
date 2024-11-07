using Microsoft.EntityFrameworkCore;
using StoreService.Api.CarritoCompra.Models;

namespace StoreService.Api.CarritoCompra.Persistence
{
    public class CarritoContext: DbContext
    {
        public CarritoContext(DbContextOptions<CarritoContext> options): base(options) { }

        public DbSet<CarritoSesion> CarritoSesions { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
