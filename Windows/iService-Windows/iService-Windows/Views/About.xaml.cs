namespace iService_Windows.Views;

public partial class About : ContentPage
{
	public About()
	{
		InitializeComponent();
	}
    private async void Button_Pressed(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}