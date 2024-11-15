using WeatherApp.Entities;
using WeatherApp.Services;

namespace WeatherApp.Pages;

public partial class CitySelectionPage : ContentPage
{
	private readonly GeocodingService _geocodingService = new GeocodingService();

    public CitySelectionPage()
	{
		InitializeComponent();
	}

    private async void btnSearch_Clicked(object sender, EventArgs e)
    {
        var cityName = CityNameEntry.Text;

        if (string.IsNullOrWhiteSpace(cityName))
        {
            await DisplayAlert("Error", "Please insert one city name.", "OK");
            return;
        }

        var cities = await _geocodingService.GetCitiesAsync(cityName);

        if(cities != null && cities.Count > 0)
        {
            CitiesListView.ItemsSource = cities;
        }
        else
        {
            await DisplayAlert("Error", "Cities not found", "OK");
        }

    }

    private async void CitiesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(e.SelectedItem is City selectedCity)
        {
            await Navigation.PushAsync(new WeatherPage(selectedCity));
        }
    }
}