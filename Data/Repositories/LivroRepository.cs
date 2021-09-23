using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class LivroRepository : CrudRepository<LivroModel>, ILivroRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public LivroRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public override async Task<IQueryable<LivroModel>> GetAllAsync()
        {
            return _bibliotecaContext
                .Livros
                .Include(x => x.Autor)
                .AsQueryable();
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
