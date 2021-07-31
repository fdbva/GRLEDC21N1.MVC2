using System;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext (DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Domain.Model.Models.AutorModel> Autores { get; set; }
        public DbSet<Domain.Model.Models.LivroModel> Livros { get; set; }
    }
}
