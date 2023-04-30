using iService3.Models;
using iService3.Tools;
using Microsoft.Maui.ApplicationModel.Communication;
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
        private HttpConnectionServer _connection;
        public UserService()
        {
            _connection = new HttpConnectionServer();
            _httpClient = _connection.GetHttpClient();
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

        public async Task<User> GetUserById(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/User/GetUserById/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(responseString);
                return user;
            }
            else
            {
                throw new Exception($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

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

        public async Task<bool> Register(string username, string email, string password)
        {
            var registerModel = new
                {
                    Username = username,
                    Email = email,
                    Pass = password
                };

                var json = JsonConvert.SerializeObject(registerModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/User/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public async Task<User> Login(string username, string password)
        {
            var url = "api/User/LogIn";

            var requestData = new
            {
                username = username,
                pass = password,
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(url, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var userObj = JsonConvert.DeserializeObject<User>(responseContent);

            return userObj;
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

        public async Task<bool> UpdateNewsletterSub(int userId, bool newsletterSub)
        {
            var data = new
            {
                userId = userId, 
                newsletterSub = newsletterSub
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/User/SetNewsletterSub/", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ResetPassword(int userId, string oldPassword, string newPassword)
        {
            var data = new
            {
                userId = userId,
                oldPassword = oldPassword,
                newPassword = newPassword
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/User/ResetPassword", content);

            return response.IsSuccessStatusCode;
        }

    }
}