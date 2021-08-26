using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IAutorHttpService
    {
        Task<IEnumerable<AutorViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<AutorViewModel> GetByIdAsync(int id);
        Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel);
        Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel);
        Task DeleteAsync(int id);
    }
}
