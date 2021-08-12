using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<LivroModel> GetByIdAsync(int id);
        Task<LivroModel> CreateAsync(LivroModel livroModel);
        Task<LivroModel> EditAsync(LivroModel livroModel);
        Task DeleteAsync(int id);
        Task<bool> IsIsbnValidAsync(string isbn, int id);
    }
}
