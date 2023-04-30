using iService3.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iService3.Models;
using iService3.Views;
using Service = iService3.Models.Service;

namespace iService3.Services
{
    class AppointmentService
    {
        private HttpClient _httpClient;
        private HttpConnectionServer _connection;
        public AppointmentService()
        {
            _connection = new HttpConnectionServer();
            _httpClient = _connection.GetHttpClient();
        }

        public async Task<List<Appointment>> GetAppointmentsByUserId(int userId)
        {
            var json = await _httpClient.GetStringAsync($"api/Appointment/GetAppointmentsByUserId/{userId}");
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
            return appointments;
        }

        public async Task<bool> ScheduleAppointment(Appointment appointment)
        {
            var requestData = new
            {
                carId = appointment.CarId,
                userId = appointment.UserId,
                appointmentDate = appointment.AppointmentDate,
                appointmentType = appointment.AppointmentType,
                appointmentNotes = appointment.AppointmentNotes,
                serviceId = appointment.ServiceId
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync("api/Appointment/ScheduleAppointment", content);

            return true;
        }

        public async Task<List<Service>> GetServices()
        {
            var json = await _httpClient.GetStringAsync("api/Service/GetServices");
            var services = JsonConvert.DeserializeObject<List<Service>>(json);
            return services;
        }
    }
}
