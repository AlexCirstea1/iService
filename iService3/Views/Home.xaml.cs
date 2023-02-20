namespace iService3.Views;

public partial class Home : ContentPage
{
    public Home()
    {
        InitializeComponent();
    }

    private async void ImageButton_Pressed(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new About());
    }

    private void Button_Pressed(object sender, EventArgs e)
    {

    }
}