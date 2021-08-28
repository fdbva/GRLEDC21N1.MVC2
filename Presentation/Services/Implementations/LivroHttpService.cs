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

        public LivroHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44395/");
        }

        public async Task<LivroViewModel> CreateAsync(LivroViewModel livroViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync("api/v1/LivroApi", livroViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroCreated = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(contentStream, JsonSerializerOptions);

            return livroCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"api/v1/LivroApi/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<LivroViewModel> EditAsync(LivroViewModel livroViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"api/v1/LivroApi/{livroViewModel.Id}", livroViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var livroEdited = await JsonSerializer
                .DeserializeAsync<LivroViewModel>(contentStream, JsonSerializerOptions);

            return livroEdited;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var livros = await _httpClient
                .GetFromJsonAsync<IEnumerable<LivroViewModel>>("/api/v1/LivroApi/");

            return livros;
        }

        public async Task<LivroViewModel> GetByIdAsync(int id)
        {
            var livros = await _httpClient
                .GetFromJsonAsync<LivroViewModel>($"/api/v1/LivroApi/{id}");

            return livros;
        }

        public async Task<bool> IsIsbnValidAsync(string isbn, int id)
        {
            var isIsbnValid = await _httpClient
                .GetFromJsonAsync<bool>($"/api/v1/LivroApi/IsIsbnValid/{isbn}/{id}");

            return isIsbnValid;
        }
    }
}
