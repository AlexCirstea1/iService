using iService3.Data;

namespace iService3;

public partial class App : Application
{
    public UserRepository UserRepository { get; private set; }
    public App(UserRepository userRepository)
    {
        InitializeComponent();

        MainPage = new AppShell();

        UserRepository = userRepository;
    }

    
}