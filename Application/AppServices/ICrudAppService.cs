using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface ICrudAppService<TViewModel>
        where TViewModel : BaseViewModel
    {
        Task<IQueryable<TViewModel>> GetAllAsync();
        Task<TViewModel> GetByIdAsync(int id);
        Task<TViewModel> CreateAsync(TViewModel viewModel);
        Task<TViewModel> EditAsync(TViewModel viewModel);
        Task DeleteAsync(int id);
    }
}
