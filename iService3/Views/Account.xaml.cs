using iService3.Models;
using iService3.Services;
using iService3.Tools;
using Newtonsoft.Json;

namespace iService3.Views;

public partial class Account : ContentPage
{
    UserService userService = new UserService();
    private SecureStorageToolKit _secureStorageToolKit;
    public Account()
    {
        _secureStorageToolKit = new SecureStorageToolKit();
        InitializeComponent();
        Load();
    }
    private async void Load()
    {
        try
        {
            usernameLabel.Text = await _secureStorageToolKit.GetUsername();
            string userJson = Preferences.Get("userData", "");
            User userData = JsonConvert.DeserializeObject<User>(userJson);
            NewsletterSwitch.IsToggled = (bool)userData.NewsletterSub;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    public static void GoToHomePage()
    {
        var mainPage = new MainPage();
        Application.Current.MainPage = mainPage;
    }

    private async void LogOutButton_OnClickedButton_Clicked(object sender, EventArgs e)
    {
        await SecureStorage.Default.SetAsync("isLogged", "false");
        SecureStorage.Default.Remove("UserID");
        SecureStorage.Default.Remove("Username");
        SecureStorage.Default.Remove("Token");
        userService.Logout();
        GoToHomePage();
    }

    private void OnAvatarTapped(object sender, TappedEventArgs e)
    {
        DisplayAlert("Profile Picture Clicked", "clicked!", "Ok","Cancel");
    }

    private async void NewsletterSwitch_OnToggled(object sender, ToggledEventArgs e)
    {
        bool isToggledOn = e.Value;
        var userid = Int32.Parse(await _secureStorageToolKit.GetUserID());
        var success = await userService.UpdateNewsletterSub(userid, isToggledOn);
    }

    private async void Button_OnClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ResetPassword(Int32.Parse(await _secureStorageToolKit.GetUserID())));
    }
}