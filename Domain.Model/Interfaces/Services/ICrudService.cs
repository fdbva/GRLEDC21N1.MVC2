using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ICrudService<TModel> : ICrudRepository<TModel>
        where TModel : BaseModel
    {
    }
}
