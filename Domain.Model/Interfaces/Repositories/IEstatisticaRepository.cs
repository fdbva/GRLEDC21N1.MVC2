using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IEstatisticaRepository
    {
        Task<HomeEstatisticaModel> GetHomeEstatisticaAsync();
    }
}
