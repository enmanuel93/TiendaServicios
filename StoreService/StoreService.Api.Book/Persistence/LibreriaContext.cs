using Microsoft.EntityFrameworkCore;
using StoreService.Api.Book.Models;

namespace StoreService.Api.Book.Persistence
{
    public class LibreriaContext: DbContext
    {
        public LibreriaContext()
        {
            
        }

        public LibreriaContext(DbContextOptions<LibreriaContext> options): base(options) { }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterials { get; set; }

    }
}
