using System.Threading.Tasks;
using Application.ViewModels;
using AutoMapper;
using Domain.Model.Interfaces.Services;

namespace Application.AppServices.Implementations
{
    public class EstatisticaAppService : IEstatisticaAppService
    {
        private readonly IEstatisticaService _estatisticaService;
        private readonly IMapper _mapper;

        public EstatisticaAppService(
            IEstatisticaService estatisticaService,
            IMapper mapper)
        {
            _estatisticaService = estatisticaService;
            _mapper = mapper;
        }

        public async Task<HomeEstatisticaViewModel> GetHomeEstatisticaAsync()
        {
            var homeEstatisticaModel = await _estatisticaService.GetHomeEstatisticaAsync();

            return _mapper.Map<HomeEstatisticaViewModel>(homeEstatisticaModel);
        }
    }
}
