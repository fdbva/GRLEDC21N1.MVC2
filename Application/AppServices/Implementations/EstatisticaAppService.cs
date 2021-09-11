using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Model.Interfaces.Services;

namespace Application.AppServices.Implementations
{
    public class EstatisticaAppService : IEstatisticaAppService
    {
        private readonly IEstatisticaService _estatisticaService;

        public EstatisticaAppService(
            IEstatisticaService estatisticaService)
        {
            _estatisticaService = estatisticaService;
        }

        public async Task<HomeEstatisticaViewModel> GetHomeEstatisticaAsync()
        {
            throw new NotImplementedException();
        }
    }
}
