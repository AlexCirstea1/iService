using iService3.Models;
using iService3.Services;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace iService3.Views;

public partial class Cars : ContentPage
{
    private readonly CarService _carService = new CarService();
    private string USER_TOKEN;
    public Cars()
    {
        InitializeComponent();
        LoadCars();
    }

    async void LoadCars()
    {
        try
        {
            var cars = await _carService.GetCarsByUserId(29);          
            foreach (var car in cars)
            {
                car.ImageUrl = await _carService.GetCarImageUrl(car.Manufacturer);
            }
            carsListView.ItemsSource = cars;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error loading cars: {ex.Message}", "OK");
        }
    }

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
        if (car != null && await DisplayAlert("Confirmation", $"Are you sure you want to delete {car.Manufacturer} {car.Model}?", "Yes", "No"))
        {
            await _carService.DeleteCar(car.CarId);
            //LoadCars();
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        LoadCars();
    }
}