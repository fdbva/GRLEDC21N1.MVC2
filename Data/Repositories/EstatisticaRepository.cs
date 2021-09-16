using System.Linq;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EstatisticaRepository : IEstatisticaRepository
    {
        private readonly BibliotecaContext _bibliotecaContext;

        public EstatisticaRepository(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;
        }

        public async Task<HomeEstatisticaModel> GetHomeEstatisticaAsync()
        {
            //maneira não performática de resolver o problema:
            //homeEstatistica.QtdAutoresCadastrados = await _bibliotecaContext.Autores.CountAsync();
            //homeEstatistica.MediaPaginaPorLivro = (await _bibliotecaContext.Livros.SumAsync(x => x.QtdPaginas)) / (double)(await _bibliotecaContext.Livros.CountAsync());
            //homeEstatistica.QtdPaginasMaiorLivro = await _bibliotecaContext.Livros.SumAsync(x => x.QtdPaginas);
            //homeEstatistica.MaiorQtdLivrosDeUmAutor = (await _bibliotecaContext.Autores.OrderByDescending(x => x.Livros.Count).Select(x => x.Livros.Count).FirstAsync());
            //homeEstatistica.MediaLivroPorAutor = await _bibliotecaContext.Livros.CountAsync() / (double)await _bibliotecaContext.Autores.CountAsync();
            //homeEstatistica.AutorMaiorLivro = (await _bibliotecaContext.Livros.OrderByDescending(x => x.QtdPaginas).Select(x => x.Autor).FirstAsync()).Nome;
            //homeEstatistica.AutorMaiorQtdLivros = (await _bibliotecaContext.Autores.OrderByDescending(x => x.Livros.Count).FirstAsync()).Nome;
            //homeEstatistica.QtdLivrosCadastrados = await _bibliotecaContext.Livros.CountAsync();

            //desta maneira abaixo, apenas uma ida ao banco é feita!
            var results = await _bibliotecaContext
                .Livros
                .TagWith("This is my special query!")
                .Select(y => new
                {
                    QtdAutores = _bibliotecaContext.Autores.Count(),
                    QtdLivros = _bibliotecaContext.Livros.Count(),
                    QtdTotalPaginasTodosLivros = _bibliotecaContext.Livros.Sum(x => x.QtdPaginas),
                    MaiorQtdLivrosDeUmAutor = _bibliotecaContext.Autores.OrderByDescending(x => x.Livros.Count).Select(x => x.Livros.Count).First(),
                    AutorMaiorLivro = _bibliotecaContext.Livros.OrderByDescending(x => x.QtdPaginas).Select(x => x.Autor).First(),
                    AutorMaiorQtdLivros = _bibliotecaContext.Autores.OrderByDescending(x => x.Livros.Count).First(),
                    QtdPaginasMaiorLivro = _bibliotecaContext.Livros.Sum(x => x.QtdPaginas)
                })
            .FirstAsync();


            var homeEstatistica = new HomeEstatisticaModel
            {
                MediaLivroPorAutor = results.QtdLivros / (double)results.QtdAutores,
                MediaPaginaPorLivro = results.QtdTotalPaginasTodosLivros / (double)results.QtdLivros,
                QtdAutoresCadastrados = results.QtdAutores,
                QtdPaginasMaiorLivro = results.QtdPaginasMaiorLivro,
                MaiorQtdLivrosDeUmAutor = results.MaiorQtdLivrosDeUmAutor,
                AutorMaiorLivro = $"{results.AutorMaiorLivro.Nome} {results.AutorMaiorLivro.UltimoNome}",
                AutorMaiorQtdLivros = $"{results.AutorMaiorLivro.Nome} {results.AutorMaiorLivro.UltimoNome}",
                QtdLivrosCadastrados = results.QtdLivros
            };

            return homeEstatistica;
        }
    }
}
