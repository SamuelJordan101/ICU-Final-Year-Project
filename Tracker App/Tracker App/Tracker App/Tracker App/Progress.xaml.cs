using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Progress : ContentPage
    {
        public ChartEntry[] entries = new[]
        {
            new ChartEntry(1)
            {
                Label = "09/2020",
                ValueLabel = "1"
            },
            new ChartEntry(2)
            {
                Label = "10/2020",
                ValueLabel = "2"
            },
            new ChartEntry((float)2.5)
            {
                Label = "11/2020",
                ValueLabel = "2.5"
            },
            new ChartEntry((float)2.8)
            {
                Label = "12/2020",
                ValueLabel = "2.8"
            }
        };

        public Progress()
        {
            InitializeComponent();
            CpaxChart.Chart = new LineChart { Entries = entries, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 30 };
        }

        async void Delete_Achievement_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Delete Achievement", "Are you sure you want to delete this achievement?", "Yes", "No");

            if (response == true)
            {
                await DisplayAlert("Done!", "Achievement has been deleted!", "Okay!");
            }
        }

    }
}