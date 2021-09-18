using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface ICrudAppService<TViewModel>
        where TViewModel : BaseViewModel
    {
        Task<IEnumerable<TViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<TViewModel> GetByIdAsync(int id);
        Task<TViewModel> CreateAsync(TViewModel viewModel);
        Task<TViewModel> EditAsync(TViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
