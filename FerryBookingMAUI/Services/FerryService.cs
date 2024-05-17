using FerryBookingClassLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FerryService
{
    private readonly HttpClient _httpClient;

    public FerryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7163/");
    }

    public async Task<IEnumerable<Ferry>> GetFerriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Ferry>>("api/ferries");
    }

    public async Task<Ferry> GetFerryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Ferry>($"api/ferries/{id}");
    }

    public async Task CreateFerryAsync(Ferry ferry)
    {
        await _httpClient.PostAsJsonAsync("api/ferries", ferry);
    }

    public async Task UpdateFerryAsync(int id, Ferry ferry)
    {
        await _httpClient.PutAsJsonAsync($"api/ferries/{id}", ferry);
    }

    public async Task DeleteFerryAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/ferries/{id}");
    }
}