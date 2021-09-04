using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IEstatisticaService
    {
        Task<HomeEstatisticaModel> GetHomeEstatisticaAsync();
    }
}
