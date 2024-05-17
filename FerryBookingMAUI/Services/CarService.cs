using FerryBookingClassLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class CarService
{
    private readonly HttpClient _httpClient;

    public CarService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7163/");
    }

    public async Task<IEnumerable<Car>> GetCarsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Car>>("api/cars");
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Car>($"api/cars/{id}");
    }

    public async Task CreateCarAsync(Car car)
    {
        await _httpClient.PostAsJsonAsync("api/cars", car);
    }

    public async Task UpdateCarAsync(int id, Car car)
    {
        await _httpClient.PutAsJsonAsync($"api/cars/{id}", car);
    }

    public async Task DeleteCarAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/cars/{id}");
    }
}