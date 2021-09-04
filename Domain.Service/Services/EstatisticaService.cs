using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class EstatisticaService : IEstatisticaService
    {
        private readonly IEstatisticaRepository _estatisticaRepository;

        public EstatisticaService(
            IEstatisticaRepository estatisticaRepository)
        {
            _estatisticaRepository = estatisticaRepository;
        }

        public async Task<HomeEstatisticaModel> GetHomeEstatisticaAsync()
        {
            var homeEstatistica = await _estatisticaRepository.GetHomeEstatisticaAsync();

            return homeEstatistica;
        }
    }
}
