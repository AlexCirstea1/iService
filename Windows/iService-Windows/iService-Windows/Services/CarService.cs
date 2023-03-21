using iService_Windows.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace iService_Windows.Services
{
    public class CarService
    {
        private HttpClient _httpClient;
        public CarService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://iservice-api.azurewebsites.net")
            };
        }

        public async Task<List<Car>> GetCars()
        {
            //HttpResponseMessage response = await _httpClient.GetAsync("api/Car/GetCars");
            //response.EnsureSuccessStatusCode();

            //List<Car> cars = await response.Content.ReadAsAsync<List<Car>>();
            //return cars;

            var json = await _httpClient.GetStringAsync("api/Car/GetCars");
            var cars = JsonConvert.DeserializeObject<List<Car>>(json);
            return cars;
        }

        public async Task<Car> GetCarById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Car/GetCarById/{id}");
            response.EnsureSuccessStatusCode();

            Car car = await response.Content.ReadAsAsync<Car>();
            return car;
        }

        public async Task<List<Car>> GetCarsByUserId(int userId)
        {
            //HttpResponseMessage response = await _httpClient.GetAsync($"api/Car/GetCarsByUserId/{userId}");
            //response.EnsureSuccessStatusCode();

            //List<Car> cars = await response.Content.ReadAsAsync<List<Car>>();
            //return cars;

            var json = await _httpClient.GetStringAsync($"api/Car/GetCarsByUserId/{userId}");
            var cars = JsonConvert.DeserializeObject<List<Car>>(json);
            return cars;
        }

        public async Task<List<Car>> GetCarsByUserToken(string token)
        {
            var json = await _httpClient.GetStringAsync($"api/Car/GetCarsByUserToken/{token}");
            var cars = JsonConvert.DeserializeObject<List<Car>>(json);
            return cars;
        }

        public async Task<Car> InsertCar(Car car)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/Car/InsertCar", car);
            response.EnsureSuccessStatusCode();

            Car newCar = await response.Content.ReadAsAsync<Car>();
            return newCar;
        }

        public async Task UpdateCar(int id, Car car)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"api/Car/UpdateCar/{id}", car);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCar(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Car/DeleteCar/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
