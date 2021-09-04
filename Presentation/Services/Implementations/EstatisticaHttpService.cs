using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class EstatisticaHttpService : IEstatisticaHttpService
    {
        private readonly HttpClient _httpClient;

        public EstatisticaHttpService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HomeEstatisticaViewModel> GetAllAsync()
        {
            var homeEstatistica = await _httpClient
                .GetFromJsonAsync<HomeEstatisticaViewModel>(string.Empty);

            return homeEstatistica;
        }
    }
}
