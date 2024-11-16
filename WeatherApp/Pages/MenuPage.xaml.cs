namespace WeatherApp.Pages;

public partial class MenuPage : FlyoutPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private void btnCitySelection_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(new CitySelectionPage());
        IsPresented = false;
    }

    private void btnFavorites_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(new FavoritePage());
        IsPresented = false;
    }

    private void btnAbout_Clicked(object sender, EventArgs e)
    {
        Detail = new NavigationPage(new AboutPage());
        IsPresented = false;
    }

    private void btnLogout_Clicked(object sender, EventArgs e)
    {
        SecureStorage.Remove("auth_token");
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}