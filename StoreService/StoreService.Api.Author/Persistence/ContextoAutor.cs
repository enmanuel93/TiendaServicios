using Microsoft.EntityFrameworkCore;
using StoreService.Api.Author.Model;

namespace StoreService.Api.Author.Persistence
{
    public class ContextoAutor: DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options): base(options) { }

        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradoAcademicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
