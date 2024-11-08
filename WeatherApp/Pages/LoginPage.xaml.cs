using WeatherApp.API;

namespace WeatherApp.Pages;

public partial class LoginPage : ContentPage
{
	private readonly LoginService _loginService = new LoginService();
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
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

			//TODO: Alterar quando tiver a weather page
			await DisplayAlert("Login", "You're Welcome", "OK");
			await Navigation.PushAsync(new MainPage());
			
		}
		else
		{
            await DisplayAlert("Erro", "Invalid credentials. Try again.", "OK");
        }
    }
}