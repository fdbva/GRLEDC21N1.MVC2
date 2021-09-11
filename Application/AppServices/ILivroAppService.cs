using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface ILivroAppService
    {
        Task<IEnumerable<LivroViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<LivroViewModel> GetByIdAsync(int id);
        Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel);
        Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsIsbnValidAsync(string isbn, int id);
    }
}
