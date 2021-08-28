﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Presentation.Models;

namespace Presentation.Services.Implementations
{
    public class AutorHttpService : IAutorHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public AutorHttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44395/");
        }

        public async Task<AutorViewModel> CreateAsync(AutorViewModel autorViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync("/api/v1/AutorApi", autorViewModel);

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorCreated = await JsonSerializer
                .DeserializeAsync<AutorViewModel>(contentStream, JsonSerializerOptions);

            return autorCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"/api/v1/AutorApi/{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<AutorViewModel> EditAsync(AutorViewModel autorViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"/api/v1/AutorApi/{autorViewModel.Id}", autorViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var autorEdited = await JsonSerializer
                .DeserializeAsync<AutorViewModel>(contentStream, JsonSerializerOptions);

            return autorEdited;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var autores = await _httpClient
                .GetFromJsonAsync<IEnumerable<AutorViewModel>>("/api/v1/AutorApi/");

            return autores;
        }

        public async Task<AutorViewModel> GetByIdAsync(int id)
        {
            var autores = await _httpClient
                .GetFromJsonAsync<AutorViewModel>($"/api/v1/AutorApi/{id}");

            return autores;
        }
    }
}
