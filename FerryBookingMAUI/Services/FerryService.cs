using FerryBookingClassLibrary.Models;
using System.Net.Http.Json;

namespace FerryBookingMAUI.Services
{
    public class FerryService
    {
        private readonly HttpClient _httpClient;

        public FerryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Ferry>> GetFerriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Ferry>>("api/Ferries");
        }

        public async Task<Ferry> GetFerryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ferry>($"api/Ferries/{id}");
        }

        public async Task CreateFerryAsync(Ferry ferry)
        {
            await _httpClient.PostAsJsonAsync("api/Ferries", ferry);
        }

        public async Task UpdateFerryAsync(int id, Ferry ferry)
        {
            await _httpClient.PutAsJsonAsync($"api/Ferries/{id}", ferry);
        }

        public async Task DeleteFerryAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Ferries/{id}");
        }
    }

}