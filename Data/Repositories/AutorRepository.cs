using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public AutorRepository(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<IEnumerable<AutorModel>> GetAllAsync(bool orderAscendant, string search = null)
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

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            var autorTask = _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .FirstOrDefaultAsync(x => x.Id == id);

            var qtdLivrosTask = _bibliotecaContext.Livros.CountAsync(x => x.AutorId == id);

            await Task.WhenAll(autorTask, qtdLivrosTask);
            //Também pode ser solucionado de maneira similar ao GetAll
            //Fizemos durante a aula a outra maneira

            var autor = await autorTask;

            autor.QuantidadeLivrosPublicados = await qtdLivrosTask;

            return autor;
        }

        public async Task<AutorModel> CreateAsync(AutorModel autorModel)
        {
            var autor = _bibliotecaContext.Autores.Add(autorModel);

            await _bibliotecaContext.SaveChangesAsync();

            return autor.Entity;
        }

        public async Task<AutorModel> EditAsync(AutorModel autorModel)
        {
            var autor = _bibliotecaContext.Autores.Update(autorModel);

            await _bibliotecaContext.SaveChangesAsync();

            return autor.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await GetByIdAsync(id);

            _bibliotecaContext.Autores.Remove(autor);

            await _bibliotecaContext.SaveChangesAsync();
        }
    }
}
