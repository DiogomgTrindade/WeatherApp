using WeatherApp.Pages;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new MenuPage();

        }


        public void SetMainPageToFlyout(Page page)
        {
            MainPage = new MenuPage();
        }
    }
}
