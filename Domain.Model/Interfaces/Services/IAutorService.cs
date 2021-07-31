using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<AutorModel> GetByIdAsync(int id);
        Task<AutorModel> CreateAsync(AutorModel autorModel);
        Task<AutorModel> EditAsync(AutorModel autorModel);
        Task DeleteAsync(int id);
    }
}
