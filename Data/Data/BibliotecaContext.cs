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
        public DbSet<Domain.Model.Models.LivroModel> Livros { get; set; }
    }
}
