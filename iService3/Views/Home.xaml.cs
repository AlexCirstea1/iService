using iService3.Services;
using iService3.Tools;

namespace iService3.Views;

public partial class Home : ContentPage
{
    private readonly SecureStorageToolKit _storageToolKit;
    private readonly AppointmentService _apointmentService;
    public Home()
    {
        _apointmentService = new AppointmentService();
        _storageToolKit = new SecureStorageToolKit();
        InitializeComponent();
        Load();
    }

    public async void Load()
    {

        if (DateTime.Now.Hour < 12)
        {
            GreetingLabel.Text = "Good Morning,";
        }
        else if (DateTime.Now.Hour < 18)
        {
            GreetingLabel.Text = "Good Afternoon,";
        }
        else
        {
            GreetingLabel.Text = "Good Evening,";
        }

        var auxString = await _storageToolKit.GetUsername();
        usernameLabel.Text = auxString.Substring(0, auxString.IndexOf(' '));
        try
        {
            var userID = Int32.Parse(await _storageToolKit.GetUserID());
            var appointments = await _apointmentService.GetAppointmentsByUserId(userID);
            foreach (var item in appointments)
            {
                item.ServiceName = item.Service.ServiceName;
                item.CarName = item.Car.Manufacturer;
            }
            appointmentsListView.ItemsSource = appointments;
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error loading appointments: {ex.Message}", "OK");
        }
    }

    private async void ImageButton_Pressed(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new About());
    }


    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ServicesPage());
    }

    private async void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new ServicesPage());
    }
}