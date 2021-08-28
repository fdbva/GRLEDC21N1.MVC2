using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class LivroHttpService : ILivroHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public LivroHttpService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, livroViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroCreated = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(contentStream, JsonSerializerOptions);

            return livroCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{livroViewModel.Id}", livroViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroEdited = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(contentStream, JsonSerializerOptions);

            return livroEdited;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var livros = await _httpClient
                .GetFromJsonAsync<IEnumerable<LivroViewModel>>($"{orderAscendant}/{search}");

            return livros;
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            var livros = await _httpClient
                .GetFromJsonAsync<LivroViewModel>($"{id}");

            return livros;
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            var isIsbnValid = await _httpClient
                .GetFromJsonAsync<bool>($"IsIsbnValid/{isbn}/{id}");

            return isIsbnValid;
        }
    }
}
