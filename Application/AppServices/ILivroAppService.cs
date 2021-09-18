using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface ILivroAppService : ICrudAppService<LivroViewModel>
    {
        Task<bool> IsIsbnValidAsync(string isbn, int id);
    }
}
