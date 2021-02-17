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
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
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