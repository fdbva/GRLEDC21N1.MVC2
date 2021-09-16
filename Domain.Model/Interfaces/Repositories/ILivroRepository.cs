using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository : IBaseRepository<LivroModel>
    {
        Task<LivroModel> GetIsbnNotFromThisIdAsync(string isbn, int id);
    }
}
