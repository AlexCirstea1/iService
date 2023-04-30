using iService3.Models;
using iService3.Services;
using iService3.Tools;

namespace iService3.Views;

public partial class ScheduleAppointment : ContentPage
{
    private readonly Car _car;
    private readonly AppointmentService _appointmentService;
    private SecureStorageToolKit _secureStorageToolKit;
    private List<string> _services = new List<string>();
    public ScheduleAppointment(Car car)
    {
        _secureStorageToolKit = new SecureStorageToolKit();
        _appointmentService = new AppointmentService();
        _car = car;
		InitializeComponent();
        LoadService();
    }

    private async void LoadService()
    {
        try
        {
            var services = await _appointmentService.GetServices();
            foreach (var item in services)
            {
                _services.Add(item.ServiceName);
            }
            Picker.ItemsSource = _services;
        }
        catch (Exception e)
        {
            await DisplayAlert("Error", e.Message, "OK");
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var userID = Int32.Parse(await _secureStorageToolKit.GetUserID());
        var services = await _appointmentService.GetServices();

        var selectedServiceIndex = Picker.SelectedIndex;
        var selectedService = services.ElementAt(selectedServiceIndex).ServiceId;

        var date = new DateTime(datePicker.Date.Year, datePicker.Date.Month, datePicker.Date.Day, timePicker.Time.Hours, timePicker.Time.Minutes, 0);
        
        var appointment = new Appointment
        {
            AppointmentDate = date,
            AppointmentType = typeEntry.Text,
            AppointmentNotes = notesEditor.Text,
            ServiceId = selectedService,
            CarId = _car.CarId,
            UserId = userID
        };
        await _appointmentService.ScheduleAppointment(appointment);
        var homepage = new Home();
        homepage.Load();
        await Navigation.PopModalAsync();
    }

    private async void CancelBtn_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}