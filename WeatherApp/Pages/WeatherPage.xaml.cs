using WeatherApp.API;

namespace WeatherApp.Pages;

public partial class WeatherPage : ContentPage
{
	private readonly WeatherService _weatherService = new WeatherService();
	private readonly double _latitude;
	private readonly double _longitude;


    public WeatherPage(double latitude, double longitude)
	{
		InitializeComponent();
		_latitude = latitude;
		_longitude = longitude;

		LoadWeatherData();
	}

    public WeatherPage()
    {
        throw new InvalidOperationException("Use the constructor with parameters (latitude and longitude).");
    }


    private async void LoadWeatherData()
    {

		var weather = await _weatherService.GetWeatherAsync(_latitude, _longitude);

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

    private void BtnRefreshWeather_Clicked(object sender, EventArgs e)
    {
		LoadWeatherData();
    }
}