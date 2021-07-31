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
            var autores = orderAscendant
                ? _bibliotecaContext.Autores.OrderBy(x => x.Nome)
                : _bibliotecaContext.Autores.OrderByDescending(x => x.Nome);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await autores.ToListAsync();
            }

            return await autores
                .Where(x=> x.Nome.Contains(search) || x.UltimoNome.Contains(search))
                .ToListAsync();
        }

        public async Task<AutorModel> GetByIdAsync(int id)
        {
            var autor = await _bibliotecaContext
                .Autores
                .Include(x => x.Livros)
                .FirstOrDefaultAsync(x => x.Id == id);

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
