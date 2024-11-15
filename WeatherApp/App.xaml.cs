using WeatherApp.Pages;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new LoginPage());
            
        }

        public void SetMainPage(Page page)
        {
            MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Colors.DarkBlue,
                BarTextColor = Colors.White
            };
        }
    }
}
