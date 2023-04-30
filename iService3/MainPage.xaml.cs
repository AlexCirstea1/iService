using iService3.Services;
using iService3.Tools;
using iService3.Views;
using Newtonsoft.Json;

namespace iService3;

public partial class MainPage : ContentPage
{
    UserService userService = new UserService();
    private SecureStorageToolKit _secureStorageToolKit;
    public MainPage()
    {
        InitializeComponent();
    }
    public void GoToAppShell()
    {
        var appShell = new AppShell();
        Application.Current.MainPage = appShell;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        var userData = await userService.Login(username, password);

        string userJson = JsonConvert.SerializeObject(userData);
        Preferences.Set("userData",userJson);

        //_secureStorageToolKit.SaveUserID(userData.UserId.ToString());
        //_secureStorageToolKit.SaveUsername(userData.Username);
        //_secureStorageToolKit.SaveToken(userData.Token);
        //testLabel.Text = _secureStorageToolKit.GetUsername().ToString();

        await SecureStorage.Default.SetAsync("UserID", userData.UserId);
        await SecureStorage.Default.SetAsync("Username", userData.Username);
        await SecureStorage.Default.SetAsync("Token", userData.Token);

        GoToAppShell();
    }

    private async void Register(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}