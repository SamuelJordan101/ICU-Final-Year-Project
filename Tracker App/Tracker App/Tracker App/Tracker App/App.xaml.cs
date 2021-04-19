using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Text;
using MySqlConnector;

namespace Tracker_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            URL();
            MainPage = new Tracker_App.Login();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        async void URL()
        {
            await SecureStorage.SetAsync("URL", "http://10.0.2.2/Tracker.API/");
        }
    }
}

