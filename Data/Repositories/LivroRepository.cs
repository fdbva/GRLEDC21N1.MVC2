using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class LivroRepository : BaseRepository<LivroModel>, ILivroRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public LivroRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public override async Task<IEnumerable<LivroModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var livros = orderAscendant
                ? _bibliotecaContext.Livros.OrderBy(x => x.Titulo)
                : _bibliotecaContext.Livros.OrderByDescending(x => x.Titulo);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await livros
                    .Include(x => x.Autor)
                    .ToListAsync();
            }

            return await livros
                .Include(x => x.Autor)
                .Where(x => x.Titulo.Contains(search))
                .ToListAsync();
        }

        public override async Task<LivroModel> GetByIdAsync(int id)
        {
            var livro = await _bibliotecaContext
                .Livros
                .Include(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            return livro;
        }

        public async Task<LivroModel> GetIsbnNotFromThisIdAsync(string isbn, int id)
        {
            var livroModel = await _bibliotecaContext
                .Livros
                .FirstOrDefaultAsync(x => x.Isbn == isbn && x.Id != id);

            return livroModel;
        }
    }
}
