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

            return livroModel == null;
        }
    }
}
