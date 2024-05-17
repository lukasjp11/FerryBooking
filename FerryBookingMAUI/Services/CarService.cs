using FerryBookingClassLibrary.Models;
using System.Net.Http.Json;

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
            return await _httpClient.GetFromJsonAsync<IEnumerable<Car>>("api/Cars");
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Car>($"api/Cars/{id}");
        }

        public async Task CreateCarAsync(Car car)
        {
            await _httpClient.PostAsJsonAsync("api/Cars", car);
        }

        public async Task UpdateCarAsync(int id, Car car)
        {
            await _httpClient.PutAsJsonAsync($"api/Cars/{id}", car);
        }

        public async Task DeleteCarAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Cars/{id}");
        }
    }

}