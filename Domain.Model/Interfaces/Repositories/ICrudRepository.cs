using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ICrudRepository<TModel>
        where TModel : BaseModel
    {
        Task<IEnumerable<TModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> EditAsync(TModel model);
        Task DeleteAsync(int id);
    }
}
