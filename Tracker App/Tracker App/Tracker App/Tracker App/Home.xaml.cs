using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        void Information_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Information());
        }

        void Goals_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Goals());
        }
        void Exercises_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Exercises());
        }
        void Progress_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Progress());
        }
        async void Logout_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Log Out", "Are you sure you want to log out?", "Yes", "No");

            if (response == true)
            {
                Preferences.Remove("PID");
                await Navigation.PushModalAsync(new Login());
            }
        }
    }
}