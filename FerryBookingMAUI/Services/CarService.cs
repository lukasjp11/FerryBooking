using FerryBookingClassLibrary.Models;
using FerryBookingClassLibrary.ViewModels;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7163/api/cars");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Car>>(content,
                new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles, PropertyNameCaseInsensitive = true
                });
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7163/api/cars/{id}");
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Car>(content,
                new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles, PropertyNameCaseInsensitive = true
                });
        }

        public async Task CreateCarAsync(Car car)
        {
            CarViewModel carViewModel = new CarViewModel
            {
                FerryId = car.FerryId, SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
            };

            StringContent content =
                new StringContent(JsonSerializer.Serialize(carViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:7163/api/cars", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCarAsync(int id, Car car)
        {
            CarViewModel carViewModel = new CarViewModel
            {
                Id = car.Id, FerryId = car.FerryId, SelectedGuestIds = car.Guests.Select(g => g.Id).ToList()
            };

            StringContent content =
                new StringContent(JsonSerializer.Serialize(carViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7163/api/cars/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCarAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7163/api/cars/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}