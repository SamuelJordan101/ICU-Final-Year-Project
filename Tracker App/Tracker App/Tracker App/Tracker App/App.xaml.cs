using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Tracker_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
    }
}
