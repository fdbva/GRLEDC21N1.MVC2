using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public class AutorAppService : IAutorAppService
    {
        private readonly IAutorService _autorService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AutorAppService(
            IAutorService autorService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _autorService = autorService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var autores = await _autorService.GetAllAsync(orderAscendant, search);

            return _mapper.Map<IEnumerable<AutorViewModel>>(autores);
        }

        public async Task<AutorViewModel> GetByIdAsync(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);

            return _mapper.Map<AutorViewModel>(autor);
        }

        public async Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            var autorModel = _mapper.Map<AutorModel>(autorViewModel);

            _unitOfWork.BeginTransaction();
            var autorModelCreated = await _autorService.CreateAsync(autorModel);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<AutorViewModel>(autorModelCreated);
        }

        public async Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel)
        {
            var autorModel = _mapper.Map<AutorModel>(autorViewModel);

            _unitOfWork.BeginTransaction();
            var autorModelCreated = await _autorService.EditAsync(autorModel);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<AutorViewModel>(autorModelCreated);
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BeginTransaction();
            await _autorService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
