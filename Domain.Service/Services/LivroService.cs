using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class LivroService : CrudService<LivroModel>, ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IIsbnValidator _isbnValidator;

        public LivroService(
            ILivroRepository livroRepository,
            IIsbnValidator isbnValidator) : base(livroRepository)
        {
            _livroRepository = livroRepository;
            _isbnValidator = isbnValidator;
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            var isIsbnValid = _isbnValidator.Validate(isbn);
            if (!isIsbnValid)
            {
                return false;
            }

            var livroModel = await _livroRepository.GetIsbnNotFromThisIdAsync(isbn, id);

            var isbnNotRepeated = livroModel == null;

            return isbnNotRepeated;
        }
    }
}