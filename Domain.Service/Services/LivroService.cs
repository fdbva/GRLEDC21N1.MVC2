using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroService : CrudService<LivroModel>, ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository) : base(livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                return false;
            }

            var livroModel = await _livroRepository.GetIsbnNotFromThisIdAsync(isbn, id);

            var digits = isbn.ToCharArray().Select(x => (int)x).ToArray();
            int i, s = 0, t = 0;

            for (i = 0; i < 10; i++)
            {
                t += digits[i];
                s += t;
            }
            var validIsbn = s % 11 == 0;

            return livroModel == null && validIsbn;
        }
    }
}
