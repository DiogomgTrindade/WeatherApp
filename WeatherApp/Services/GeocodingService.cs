using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Entities;

namespace WeatherApp.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "aed6ef0c74a949e4bb59678380d37950";
        private const string BaseUrl = "https://api.openweathermap.org/geo/1.0/";

        public async Task<List<City>> GetCitiesAsync(string cityName, string countryCode = "", int limit = 5)
        {
            try
            {
                var url = $"{BaseUrl}direct?q={cityName},{countryCode}&limit={limit}&appid={ApiKey}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<City>>(json);
                }
                else
                {
                    Console.WriteLine($"Erro ao chamar a API: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de conexão: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro de Conexão", "Não foi possível conectar ao servidor. Verifique sua conexão com a internet.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro desconhecido: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro ao buscar dados. Tente novamente mais tarde.", "OK");
            }

            return null;

        }
    }
}
