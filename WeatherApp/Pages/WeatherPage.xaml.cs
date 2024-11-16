using WeatherApp.API;
using WeatherApp.Entities;
using WeatherApp.Services;

namespace WeatherApp.Pages;

public partial class WeatherPage : ContentPage
{
	private readonly WeatherService _weatherService = new WeatherService();
	private readonly FavoriteService _favoriteService = new FavoriteService();
	private readonly City _currentCity;


    public WeatherPage(City city)
	{
		InitializeComponent();
		_currentCity = city;

		CityNameLabel.Text = $"City: {city.Name}";
		
	}

   

    public WeatherPage()
    {
        throw new InvalidOperationException("Use the constructor with parameters (latitude and longitude).");
    }


    private async void LoadWeatherData()
    {

		var weather = await _weatherService.GetWeatherAsync(_currentCity.Lat, _currentCity.Lon);

		if (weather != null)
		{
			TemperatureLabel.Text = $"Temperature: {weather.Main.Temp}ºC";
			DescriptionLabel.Text = $"Condition: {weather.Weather[0].Description}";
			HumidityLabel.Text = $"Humidity: {weather.Main.Humidty}%";
		}
		else
		{
			await DisplayAlert("Error", "Unable to retrieve weather data.", "OK");
		}
    }

    private async void BtnRefreshWeather_Clicked(object sender, EventArgs e)
    {
        LoadWeatherData();
		await DisplayAlert("Success", "Weather refreshed successfully!", "OK");
    }


    private void UpdateFavoriteButton()
    {
        if (_currentCity.isFavorite)
		{
			btnFavorite.Source = "heart_filled.svg";
		}
		else
		{
            btnFavorite.Source = "heart_outline.svg";
		}
    }

    private async void btnFavorite_Clicked(object sender, EventArgs e)
    {
        if (await _favoriteService.IsFavoriteAsync(_currentCity))
        {
            await _favoriteService.RemoveFromFavoritesAsync(_currentCity);
            await DisplayAlert("Removed", $"{_currentCity.Name} was removed from your favorites.", "OK");
        }
        else
        {
            await _favoriteService.AddToFavoritesAsync(_currentCity);
            await DisplayAlert("Added", $"{_currentCity.Name} was added to your favorites.", "OK");
        }

        UpdateFavoriteButton();
    }

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        LoadWeatherData();

        var isFavorite = await _favoriteService.IsFavoriteAsync(_currentCity);
        _currentCity.isFavorite = isFavorite;

        UpdateFavoriteButton();
    }
}