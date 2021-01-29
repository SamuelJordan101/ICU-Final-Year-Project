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
    public partial class Exercises : ContentPage
    {
        public Exercises()
        {
            InitializeComponent();
        }

        void Exercise_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Exercise_Individual());
        }
    }
}