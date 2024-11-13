using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Entities;

namespace WeatherApp.API
{
    public class WeatherService 
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "aed6ef0c74a949e4bb59678380d37950";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<WeatherResponse> GetWeatherAsync(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}&appid={ApiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherResponse>(json);
            }

            return null;
        }
    }
}
