using FerryBookingClassLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;

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
        var content = new StringContent(JsonSerializer.Serialize(car), System.Text.Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("https://localhost:7163/api/cars", content);
    }

    public async Task UpdateCarAsync(int id, Car car)
    {
        var content = new StringContent(JsonSerializer.Serialize(car), System.Text.Encoding.UTF8, "application/json");
        await _httpClient.PutAsync($"https://localhost:7163/api/cars/{id}", content);
    }

    public async Task DeleteCarAsync(int id)
    {
        await _httpClient.DeleteAsync($"https://localhost:7163/api/cars/{id}");
    }
}
