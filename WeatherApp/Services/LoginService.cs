using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp.API
{
    public class LoginService
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private const string AuthBaseUrl = "https://globeairlinesdmgt.somee.com/api";

        public async Task<string> LoginAsync(string username, string password)
        {
            var loginData = new { EmailAddress = username, Password = password };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{AuthBaseUrl}/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                var token = tokenObj.token.ToString();
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                return token;
            }

            return null;
        }


        public async Task<bool> RegisterAsync(string username, string password, string firstName, string lastName)
        {
            var registerData = new { EmailAdress = username, Password = password, FirstName = firstName, LastName = lastName };
            var json = JsonConvert.SerializeObject(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{AuthBaseUrl}/register", content);
            return response.IsSuccessStatusCode;
        }

        public void SetAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        }
    }
}
