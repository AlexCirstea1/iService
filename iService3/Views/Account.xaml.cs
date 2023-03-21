using iService3.Models;
using iService3.Services;
using Microsoft.Extensions.Caching.Memory;
using SQLite;
using System.Text;
using System.Text.Json;

namespace iService3.Views;

public partial class Account : ContentPage
{
    UserService userService = new UserService();

    private MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

    public Account()
    {
        InitializeComponent();
        
        
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        User user = new User()
        {
            Username = username,
            Pass = password
        };

        var userData = await userService.Login(user);

        var userID = userData.UserId;
        var userName = userData.Username;
        var token = userData.Token;



        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));

        cache.Set("username", userName, cacheEntryOptions);
        cache.Set("userID", userID, cacheEntryOptions);
        cache.Set("token", token, cacheEntryOptions);
    }

    private void RegisterButton_Clicked(object sender, EventArgs e)
    {

    }

    private async void LogOut_Clicked(object sender, EventArgs e)
    {
        var logout = await userService.Logout();
        cache.Dispose();
    }
}