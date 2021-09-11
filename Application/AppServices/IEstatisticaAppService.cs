using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.AppServices
{
    public interface IEstatisticaAppService
    {
        Task<HomeEstatisticaViewModel> GetHomeEstatisticaAsync();
    }
}
