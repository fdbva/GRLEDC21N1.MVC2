using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class LivroAppService : CrudAppService<LivroModel, LivroViewModel>, ILivroAppService
    {
        private readonly ILivroService _livroService;

        public LivroAppService(
            ILivroService livroService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            : base(livroService, mapper, unitOfWork)
        {
            _livroService = livroService;
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            return await _livroService.IsIsbnValidAsync(isbn, id);
        }
    }
}
