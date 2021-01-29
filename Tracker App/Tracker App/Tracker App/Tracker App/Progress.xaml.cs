using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;

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
                ValueLabel = "1",
                Color = SKColor.Parse("#ff0000")
            },
            new ChartEntry(2)
            {
                Label = "10/2020",
                ValueLabel = "2",
                Color = SKColor.Parse("#ff003e")
            },
            new ChartEntry((float)2.5)
            {
                Label = "11/2020",
                ValueLabel = "2.5",
                Color = SKColor.Parse("#ff0070")
            },
            new ChartEntry((float)2.8)
            {
                Label = "12/2020",
                ValueLabel = "2.8",
                Color = SKColor.Parse("#f00094")
            },
            new ChartEntry(3)
            {
                Label = "01/2021",
                ValueLabel = "3",
                Color = SKColor.Parse("#b800cd")
            },
            new ChartEntry((float)3.1)
            {
                Label = "02/2021",
                ValueLabel = "3.1",
                Color = SKColor.Parse("#0000ff")
            }
        };

        public Progress()
        {
            InitializeComponent();
            CpaxChart.Chart = new LineChart { Entries = entries, LabelTextSize = 40, MaxValue=5, PointSize=35, LineSize=10, LabelColor= SKColor.Parse("#000000") };
        }

        async void Delete_Achievement_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Delete Achievement", "Are you sure you want to delete this achievement?", "Yes", "No");

            if (response == true)
            {
                await DisplayAlert("Done!", "Achievement has been deleted!", "Okay!");
            }
        }

        async void Add_Achievement_Button(object sender, System.EventArgs e)
        {
            await DisplayAlert("Done!", "Achievement has been added!", "Okay!");
        }

    }
}