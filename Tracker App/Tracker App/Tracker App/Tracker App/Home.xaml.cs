using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        void Settings_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Settings());
        }
    }
}