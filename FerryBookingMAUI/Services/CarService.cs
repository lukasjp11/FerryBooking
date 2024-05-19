using FerryBookingClassLibrary.Models;
using FerryBookingClassLibrary.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace FerryBookingMAUI.Services
{
    public class CarService
    {
        private readonly HttpClient _httpClient;

        public CarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7163/api/cars");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Car>>(content, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7163/api/cars/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Car>(content, new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task CreateCarAsync(Car car)
        {
            var carViewModel = new CarViewModel
            {
                FerryId = car.FerryId,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
            };

            var content = new StringContent(JsonSerializer.Serialize(carViewModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7163/api/cars", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCarAsync(int id, Car car)
        {
            var carViewModel = new CarViewModel
            {
                Id = car.Id,
                FerryId = car.FerryId,
                SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
            };

            var content = new StringContent(JsonSerializer.Serialize(carViewModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7163/api/cars/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCarAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7163/api/cars/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
