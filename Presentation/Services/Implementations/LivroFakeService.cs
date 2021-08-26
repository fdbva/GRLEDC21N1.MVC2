using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class LivroFakeService : ILivroHttpService
    {
        private static List<LivroViewModel> Livros { get; } = new List<LivroViewModel>
        {
            new LivroViewModel
            {
                Id = 0,
                Isbn = "1123123123",
                Titulo = "1123123123",
                Lancamento = DateTime.Now
            },
            new LivroViewModel
            {
                Id = 1,
                Isbn = "565656565",
                Titulo = "565656565",
                Lancamento = new DateTime(2011, 01, 03)
            }
        };

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Livros;
            }

            var resultByLinq = Livros
                .Where(x => x.Titulo.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                ? resultByLinq.OrderBy(x => x.Titulo)
                : resultByLinq.OrderByDescending(x => x.Titulo);

            return resultByLinq;
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            foreach (var livro in Livros)
            {
                if (livro.Id == id)
                {
                    return livro;
                }
            }

            return null;
        }

        private static int _id = Livros.Max(x => x.Id);
        private int Id => Interlocked.Increment(ref _id);
        public async Task<LivroViewModel> CreateAsync(LivroViewModel LivroViewModel)
        {
            LivroViewModel.Id = Id;

            Livros.Add(LivroViewModel);

            //TODO: auto-increment Id e atualizar para o retorno
            return LivroViewModel;
        }

        public async Task<LivroViewModel> EditAsync(LivroViewModel LivroViewModel)
        {
            foreach (var livro in Livros)
            {
                if (livro.Id == LivroViewModel.Id)
                {
                    livro.Isbn = LivroViewModel.Isbn;
                    livro.Titulo = LivroViewModel.Titulo;
                    livro.Lancamento = LivroViewModel.Lancamento;
                    livro.AutorId = LivroViewModel.AutorId;

                    return livro;
                }
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            LivroViewModel livroEncontrado = null;
            foreach (var livro in Livros)
            {
                if (livro.Id == id)
                {
                    livroEncontrado = livro;
                }
            }

            if (livroEncontrado != null)
            {
                Livros.Remove(livroEncontrado);
            }
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            throw new NotImplementedException();
        }
    }
}
