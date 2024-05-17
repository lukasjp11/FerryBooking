using FerryBookingClassLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class GuestService
{
    private readonly HttpClient _httpClient;

    public GuestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7163/");
    }

    public async Task<IEnumerable<Guest>> GetGuestsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Guest>>("api/guests");
    }

    public async Task<Guest> GetGuestByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Guest>($"api/guests/{id}");
    }

    public async Task CreateGuestAsync(Guest guest)
    {
        await _httpClient.PostAsJsonAsync("api/guests", guest);
    }

    public async Task UpdateGuestAsync(int id, Guest guest)
    {
        await _httpClient.PutAsJsonAsync($"api/guests/{id}", guest);
    }

    public async Task DeleteGuestAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/guests/{id}");
    }
}