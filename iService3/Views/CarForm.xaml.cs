using iService3.Models;
using iService3.Services;
using iService3.Tools;

namespace iService3.Views;

public partial class CarForm : ContentPage
{
    private readonly CarService _carService = new();
    private List<string> _carManufacturerList = new();
    private readonly SecureStorageToolKit _secureStorageToolKit;
    public CarForm()
	{
        _secureStorageToolKit = new SecureStorageToolKit();
        InitializeComponent();
        LoadCarManufacturer();
    }

    private async Task LoadCarManufacturer()
    {
        try
        {
            _carManufacturerList = await _carService.GetCarManufacturers();
            _carManufacturerList = _carManufacturerList.OrderBy(q=>q).ToList();
            manufacturerPicker.ItemsSource = _carManufacturerList;
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void addCarBtn_Clicked(object sender, EventArgs e)
    {
		Car car = new Car()
		{
            UserId = Int32.Parse(await _secureStorageToolKit.GetUserID()),
            Manufacturer = manufacturerPicker.SelectedItem.ToString(),
			Model = Model.Text,
			Year = int.Parse(Year.Text)
		};
        await _carService.AddCar(car);

    }
}