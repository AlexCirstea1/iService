namespace iService3.Views;

public partial class Cars : ContentPage
{
    public Cars()
    {
        InitializeComponent();
    }

    private async void addBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CarForm());
    }
}