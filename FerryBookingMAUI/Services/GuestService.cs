using FerryBookingClassLibrary.Models;
using System.Net.Http.Json;

namespace FerryBookingMAUI.Services
{
    public class GuestService
    {
        private readonly HttpClient _httpClient;

        public GuestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Guest>>("api/Guests");
        }

        public async Task<Guest> GetGuestByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Guest>($"api/Guests/{id}");
        }

        public async Task CreateGuestAsync(Guest guest)
        {
            await _httpClient.PostAsJsonAsync("api/Guests", guest);
        }

        public async Task UpdateGuestAsync(int id, Guest guest)
        {
            await _httpClient.PutAsJsonAsync($"api/Guests/{id}", guest);
        }

        public async Task DeleteGuestAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Guests/{id}");
        }
    }

}