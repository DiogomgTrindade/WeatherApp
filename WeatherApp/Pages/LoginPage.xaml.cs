using WeatherApp.API;

namespace WeatherApp.Pages;

public partial class LoginPage : ContentPage
{
	private readonly LoginService _loginService = new LoginService();
	public LoginPage()
	{
		InitializeComponent();
        CheckInternetConnectivity();
	}

   

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Email or password cannot be empty.", "OK");
            return;
        }

        var token = await _loginService.LoginAsync(username, password);
        if (token != null)
        {
            await SecureStorage.SetAsync("auth_token", token);
            _loginService.SetAuthorizationHeader(token);

            var app = Application.Current as App;
            app?.SetMainPageToFlyout(new MenuPage());
        }
        else
        {
            await DisplayAlert("Erro", "Invalid credentials. Try again.", "OK");
        }
    }

    private async void CheckInternetConnectivity()
    {
        if(Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No internet connection", "You are not connected to internet, please check your connection.", "OK");
            btnLogin.IsEnabled = false;
            lblInternet.IsVisible = true;
        }
    }

}