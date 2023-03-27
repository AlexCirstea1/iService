using iService3.Models;
using iService3.Services;

namespace iService3.Views;

public partial class CarForm : ContentPage
{
    private readonly CarService _carService = new CarService();
    public CarForm()
	{
		InitializeComponent();
	}

    private async void addCarBtn_Clicked(object sender, EventArgs e)
    {
		Car car = new Car()
		{
			Manufacturer = Manufacturer.Text,
			Model = Model.Text,
			Year = int.Parse(Year.Text)
		};
		var isSuccess = await _carService.InsertCar(car);
        if (isSuccess)
        {
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Error", $"Error saving the data", "OK");
        }
        
    }
}