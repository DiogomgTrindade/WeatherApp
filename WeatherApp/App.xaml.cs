using WeatherApp.Pages;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navPage = new NavigationPage(new LoginPage());

            navPage.BarBackground = Colors.DarkBlue;
            navPage.BarTextColor = Colors.White;

            MainPage = navPage;
            
        }
    }
}
