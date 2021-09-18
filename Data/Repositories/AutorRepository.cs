using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Data.Repositories
{
    public class AutorRepository : CrudRepository<AutorModel>, IAutorRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public AutorRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public override async Task<IEnumerable<AutorModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var autores = _bibliotecaContext.Autores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                autores = autores
                    .Where(x => x.Nome.Contains(search) || x.UltimoNome.Contains(search));
            }

            autores = orderAscendant
                ? autores.OrderBy(x => x.Nome)
                : autores.OrderByDescending(x => x.Nome);

            var result = await autores
                .Select(x => new
                {
                    Autor = x,
                    QtdLivros = x.Livros.Count
                })
                .ToListAsync();

            var autoresResult = result
                .Select(x =>
                {
                    x.Autor.QuantidadeLivrosPublicados = x.QtdLivros;
                    return x.Autor;
                });

            return autoresResult;
        }

        public override async Task<AutorModel> GetByIdAsync(int id)
        {
            //https://entityframework-plus.net/query-future
            var autorFuture = _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .DeferredFirstOrDefault(x => x.Id == id)
                .FutureValue();

            var qtdLivrosFuture = _bibliotecaContext
                .Livros
                .DeferredCount(x => x.AutorId == id)
                .FutureValue();

            var autor = await autorFuture.ValueAsync();

            autor.QuantidadeLivrosPublicados = await qtdLivrosFuture.ValueAsync();

            return autor;
        }
    }
}
