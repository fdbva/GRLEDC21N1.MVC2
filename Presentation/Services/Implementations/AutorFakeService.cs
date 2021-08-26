using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class AutorFakeService : IAutorHttpService
    {
        private static List<AutorViewModel> Autores { get; } = new List<AutorViewModel>
        {
            new AutorViewModel
            {
                Id = 0,
                Nome = "Felipe",
                UltimoNome = "Andrade",
                Nacionalidade = "Brasileiro",
                Nascimento = new DateTime(1988, 02, 23),
                QuantidadeLivrosPublicados = 0
            },
            new AutorViewModel
            {
                Id = 1,
                Nome = "Felipe2",
                UltimoNome = "Andrade2",
                Nacionalidade = "Brasileiro2",
                Nascimento = new DateTime(2000, 02, 23),
                QuantidadeLivrosPublicados = 0
            }
        };

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Autores;
            }

            var resultByLinq = Autores
                .Where(x =>
                    x.Nome.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    x.UltimoNome.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                ? resultByLinq.OrderBy(x => x.Nome).ThenBy(x => x.UltimoNome)
                : resultByLinq.OrderByDescending(x => x.Nome).ThenByDescending(x => x.UltimoNome);

            return resultByLinq;
        }

        public async Task<AutorViewModel> GetByIdAsync(int id)
        {
            foreach (var autor in Autores)
            {
                if (autor.Id == id)
                {
                    return autor;
                }
            }

            return null;
        }

        private static int _id = Autores.Max(x => x.Id);
        private int Id => Interlocked.Increment(ref _id);
        public async Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            autorViewModel.Id = Id;

            Autores.Add(autorViewModel);

            //TODO: auto-increment Id e atualizar para o retorno
            return autorViewModel;
        }

        public async Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel)
        {
            foreach (var autor in Autores)
            {
                if (autor.Id == autorViewModel.Id)
                {
                    autor.Nacionalidade = autorViewModel.Nacionalidade;
                    autor.Nome = autorViewModel.Nome;
                    autor.UltimoNome = autorViewModel.UltimoNome;
                    autor.Nascimento = autorViewModel.Nascimento;
                    autor.QuantidadeLivrosPublicados = autorViewModel.QuantidadeLivrosPublicados;

                    return autor;
                }
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            AutorViewModel autorEncontrado = null;
            foreach (var autor in Autores)
            {
                if (autor.Id == id)
                {
                    autorEncontrado = autor;
                }
            }

            if (autorEncontrado != null)
            {
                Autores.Remove(autorEncontrado);
            }
        }
    }
}
