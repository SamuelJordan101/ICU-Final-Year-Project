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
    public partial class Goals : ContentPage
    {
        public Goals()
        {
            InitializeComponent();
        }

        async void Done_Goal_Button(object sender, System.EventArgs e)
        {
            await DisplayAlert("Completed!", "Goal Has Been Completed!", "Okay!");
        }

        async void Delete_Goal_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Delete Goal", "Are you sure you want to delete this goal?", "Yes", "No");

            if (response == true)
            {
                await DisplayAlert("Done!", "Goal has been deleted!", "Okay!");
            }
        }

        async void Add_Goal_Button(object sender, System.EventArgs e)
        {
            await DisplayAlert("Done!", "Goal has been added!", "Okay!");
        }


    }
}