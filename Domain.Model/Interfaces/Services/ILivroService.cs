using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface ILivroService : ICrudService<LivroModel>
    {
        Task<bool> IsIsbnValidAsync(string isbn, int id);
    }
}
