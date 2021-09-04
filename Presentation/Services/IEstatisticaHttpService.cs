using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services
{
    public interface IEstatisticaHttpService
    {
        Task<HomeEstatisticaViewModel> GetAllAsync();
    }
}
