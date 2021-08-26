using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface ILivroHttpService
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
