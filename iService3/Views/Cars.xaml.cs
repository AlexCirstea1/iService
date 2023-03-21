using iService3.Models;
using iService3.Services;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace iService3.Views;

public partial class Cars : ContentPage
{
    private readonly CarService _carService = new CarService();
    private string USER_TOKEN;
    private MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
    public Cars()
    {
        InitializeComponent();
        LoadCars();
    }

    private void GetUserToken()
    {
        cache.TryGetValue("token", out string token);
        if (token != null)
        {
            USER_TOKEN = token;
        }
    }

    async void LoadCars()
    {

        carsListView.ItemsSource = null;
        try
        {
            GetUserToken();
            if(USER_TOKEN != null)
            {
                var cars = await _carService.GetCarsByUserToken(USER_TOKEN);
                carsListView.ItemsSource = cars;
            }          
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error loading cars: {ex.Message}", "OK");
        }
    }

    //[Obsolete]
    //private void LoadCars()
    //{
    //    Task.Run(async () =>
    //    {
    //        List<Car> cars = await _carService.GetCars();
    //        Device.BeginInvokeOnMainThread(() =>
    //        {
    //            carsListView.ItemsSource = cars;
    //        });
    //    });
    //}
    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CarForm());
    }

    private void UpdateBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void ScheduleAppointmentBtn_Clicked(object sender, EventArgs e)
    {

    }

    private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
        var car = (sender as Button)?.BindingContext as Car;

        if(car != null)
        {
            await _carService.DeleteCar(car.CarId);
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        
    }
}