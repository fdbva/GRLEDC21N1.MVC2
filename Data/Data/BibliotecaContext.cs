using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Model.Models.AutorModel> Autores { get; set; }
    }
}
