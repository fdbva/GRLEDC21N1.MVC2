using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ICrudRepository<TModel>
        where TModel : BaseModel
    {
        Task<IQueryable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> CreateAsync(TModel model);
        Task<TModel> EditAsync(TModel model);
        Task DeleteAsync(int id);
    }
}
