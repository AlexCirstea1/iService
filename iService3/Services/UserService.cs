using iService3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iService3.Services
{
    internal class UserService
    {
        private HttpClient _httpClient;
        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://iservice-api.azurewebsites.net")
            };
        }
        //public async Task<List<User>> GetAllUsers()
        //{
        //    var response = await _httpClient.GetAsync($"api/User/GetUsers");
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }
        //    var content = await response.Content.ReadAsStringAsync();
        //    var userList = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    return userList;
        //}

        //public async Task<User> GetUserById(int id)
        //{
        //    var response = await _httpClient.GetAsync($"api/GetUserById/{id}");
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }
        //    var content = await response.Content.ReadAsStringAsync();
        //    var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    return user;
        //}

        //public async Task<List<Appointment>> GetUserAppointments(int id)
        //{
        //    var response = await _httpClient.GetAsync($"api/GetUserAppointments/{id}");
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }
        //    var content = await response.Content.ReadAsStringAsync();
        //    var appointmentList = JsonSerializer.Deserialize<List<Appointment>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    return appointmentList;
        //}

        //public async Task<User> Register(User user)
        //{
        //    var response = await _httpClient.PostAsJsonAsync($"api/Register", user);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        return null;
        //    }
        //    var content = await response.Content.ReadAsStringAsync();
        //    var result = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //    return result;
        //}

        public async Task<string> Login(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/LogIn", content);
          
            var responseString = await response.Content.ReadAsStringAsync();

            var tokenModel = JsonConvert.DeserializeObject<string>(responseString);

            return tokenModel;
        }

        public async Task<bool> Logout()
        {
            var response = await _httpClient.PostAsync($"api/User/LogOut", null);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/DeleteUser/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}