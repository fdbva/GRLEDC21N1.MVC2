using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface IAutorAppService
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
