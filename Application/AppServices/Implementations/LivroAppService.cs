using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class LivroAppService : ILivroAppService
    {
        private readonly ILivroService _livroService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LivroAppService(
            ILivroService livroService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _livroService = livroService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var livros = await _livroService.GetAllAsync(orderAscendant, search);

            return _mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);

            return _mapper.Map<LivroViewModel>(livro);
        }

        public async Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel)
        {
            var livroModel = _mapper.Map<LivroModel>(livroViewModel);

            _unitOfWork.BeginTransaction();
            var livroModelCreated = await _livroService.CreateAsync(livroModel);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LivroViewModel>(livroModelCreated);
        }

        public async Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel)
        {
            var livroModel = _mapper.Map<LivroModel>(livroViewModel);

            _unitOfWork.BeginTransaction();
            var livroModelCreated = await _livroService.EditAsync(livroModel);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<LivroViewModel>(livroModelCreated);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BeginTransaction();
            await _livroService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            return await _livroService.IsIsbnValidAsync(isbn, id);
        }
    }
}
