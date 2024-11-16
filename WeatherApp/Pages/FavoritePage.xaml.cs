using WeatherApp.Entities;
using WeatherApp.Services;

namespace WeatherApp.Pages;

public partial class FavoritePage : ContentPage
{
	private readonly FavoriteService _favoriteService = new FavoriteService();
	public FavoritePage()
	{
		InitializeComponent();
		LoadFavorites();
	}

    private async void LoadFavorites()
    {
        var favorites = await _favoriteService.GetFavoriteCitiesAsync();
        FavoritesListView.ItemsSource = favorites;
    }

    private async void FavoritesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is City selectedCity)
        {
            var menuPage = Application.Current.MainPage as MenuPage;
            if (menuPage != null)
            {
                menuPage.Detail = new NavigationPage(new WeatherPage(selectedCity));
                menuPage.IsPresented = false;
            }

        }
    }
}