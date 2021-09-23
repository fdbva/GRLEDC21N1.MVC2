using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public abstract class CrudService<TModel> : ICrudService<TModel>
        where TModel : BaseModel
    {
        private readonly ICrudRepository<TModel> _crudRepository;

        protected CrudService(
            ICrudRepository<TModel> crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public virtual async Task<IQueryable<TModel>> GetAllAsync()
        {
            return await _crudRepository.GetAllAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            return await _crudRepository.GetByIdAsync(id);
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            return await _crudRepository.CreateAsync(model);
        }

        public virtual async Task<TModel> EditAsync(TModel model)
        {
            return await _crudRepository.EditAsync(model);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _crudRepository.DeleteAsync(id);
        }
    }
}
