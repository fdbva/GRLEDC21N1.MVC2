using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;
using Domain.Model.Interfaces.UoW;
using Domain.Model.Models;

namespace Application.AppServices.Implementations
{
    public abstract class CrudAppService<TModel, TViewModel> : ICrudAppService<TViewModel>
        where TModel : BaseModel
        where TViewModel : BaseViewModel
    {
        private readonly ICrudService<TModel> _crudService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        protected CrudAppService(
            ICrudService<TModel> crudService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _crudService = crudService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<IQueryable<TViewModel>> GetAllAsync()
        {
            var models = await _crudService.GetAllAsync();

            return _mapper.ProjectTo<TViewModel>(models);
        }

        public virtual async Task<TViewModel> GetByIdAsync(int id)
        {
            var model = await _crudService.GetByIdAsync(id);

            return _mapper.Map<TViewModel>(model);
        }

        public virtual async Task<TViewModel> CreateAsync(TViewModel viewModel)
        {
            var model = _mapper.Map<TModel>(viewModel);

            _unitOfWork.BeginTransaction();
            var modelCreated = await _crudService.CreateAsync(model);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TViewModel>(modelCreated);
        }

        public virtual async Task<TViewModel> EditAsync(TViewModel viewModel)
        {
            var model = _mapper.Map<TModel>(viewModel);

            _unitOfWork.BeginTransaction();
            var modelCreated = await _crudService.EditAsync(model);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TViewModel>(modelCreated);
        }

        public virtual async Task DeleteAsync(int id)
        {
            _unitOfWork.BeginTransaction();
            await _crudService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}
