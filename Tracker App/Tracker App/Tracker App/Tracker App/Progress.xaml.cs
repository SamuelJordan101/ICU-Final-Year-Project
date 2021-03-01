using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Flurl;
using Flurl.Http;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Progress : ContentPage
    {


        public ChartEntry[] entries = new[]
        {
            new ChartEntry(1)
            {
                Label = "September",
                ValueLabel = "1",
                Color = SKColor.Parse("#ff0000")
            },
            new ChartEntry(2)
            {
                Label = "October",
                ValueLabel = "2",
                Color = SKColor.Parse("#ff003e")
            },
            new ChartEntry((float)2.5)
            {
                Label = "November",
                ValueLabel = "2.5",
                Color = SKColor.Parse("#ff0070")
            },
            new ChartEntry((float)2.8)
            {
                Label = "December",
                ValueLabel = "2.8",
                Color = SKColor.Parse("#f00094")
            },
            new ChartEntry(3)
            {
                Label = "January",
                ValueLabel = "3",
                Color = SKColor.Parse("#b800cd")
            },
            new ChartEntry((float)3.1)
            {
                Label = "February",
                ValueLabel = "3.1",
                Color = SKColor.Parse("#0000ff")
            }
        };

        public Progress()
        {
            InitializeComponent();
            LoadInfo();
            CPAXChart();
        }

        public class achievement
        {
            public int? Id { get; set; }
            public int PatientId { get; set; }
            public string Achievement1 { get; set; }
        }

        public class cpax
        {
            public int Id { get; set; }
            public int PatientID { get; set; }
            public DateTime CPAXDate { get; set; }
            public int Grip { get; set; }
            public int Respiratory { get; set; }
            public int Cough { get; set; }
            public int BedMovement { get; set; }
            public int DynamicSitting { get; set; }
            public int StandingBalance { get; set; }
            public int SitToStand { get; set; }
            public int BedToChair { get; set; }
            public int Stepping { get; set; }
            public int Transfer { get; set; }
        }

        async void CPAXChart()
        {
            var ID = Preferences.Get("PID", "");
            var URL = await SecureStorage.GetAsync("URL");
            List<cpax> tempCpax = await URL.AppendPathSegment("CPAX").AppendPathSegment(ID).GetJsonAsync<List<cpax>>();

            int length;

            if (tempCpax.Count() < 5)
                length = tempCpax.Count();
            else
                length = 5;

            int[,] CPAXScores = new int[5, 2];

            for(var i = 0; i < length; i++)
            {
                CPAXScores[i, 0] = tempCpax[i].CPAXDate.Month;
                CPAXScores[i, 1] = (tempCpax[i].Grip + tempCpax[i].Respiratory + tempCpax[i].Cough + tempCpax[i].BedMovement + tempCpax[i].DynamicSitting + tempCpax[i].StandingBalance + tempCpax[i].SitToStand +
                    tempCpax[i].BedToChair + tempCpax[i].Stepping + tempCpax[i].Transfer) / 10;
            }

            string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string[] CPAXMonths = new string[5];


            for (var i = 0; i < length; i++)
                if (CPAXScores[i, 0] != 0)
                    CPAXMonths[i] = Months[CPAXScores[i, 0]-1];
                else
                    CPAXMonths[i] = "";

            ChartEntry[] entries = new[]
            {
                new ChartEntry(CPAXScores[0,1])
                {
                    Label = CPAXScores[0,0].ToString(),
                    ValueLabel = CPAXMonths[0],
                    Color = SKColor.Parse("#ff0000")
                },
                new ChartEntry(CPAXScores[1,1])
                {
                    Label = CPAXScores[1,0].ToString(),
                    ValueLabel = CPAXMonths[1],
                    Color = SKColor.Parse("#ff0000")
                },
                new ChartEntry(CPAXScores[2,1])
                {
                    Label = CPAXScores[2,0].ToString(),
                    ValueLabel = CPAXMonths[2],
                    Color = SKColor.Parse("#ff0000")
                },
                new ChartEntry(CPAXScores[3,1])
                {
                    Label = CPAXScores[3,0].ToString(),
                    ValueLabel = CPAXMonths[3],
                    Color = SKColor.Parse("#ff0000")
                },
                new ChartEntry(CPAXScores[4,1])
                {
                    Label = CPAXScores[4,0].ToString(),
                    ValueLabel = CPAXMonths[4],
                    Color = SKColor.Parse("#ff0000")
                },
            };

            CpaxChart.Chart = new LineChart { Entries = entries, LabelTextSize = 34, MaxValue = 5, PointSize = 35, LineSize = 10, LabelColor = SKColor.Parse("#000000"), LabelOrientation = Orientation.Horizontal ,ValueLabelOrientation = Orientation.Horizontal, BackgroundColor = SKColors.Transparent };
            UserDialogs.Instance.HideLoading();
        }

        async void LoadInfo()
        {
            UserDialogs.Instance.ShowLoading("Loading Progress...");
            var ID = Preferences.Get("PID", "");
            var URL = await SecureStorage.GetAsync("URL");

            List<achievement> UserData = await URL.AppendPathSegment("Achievement").AppendPathSegment(ID).GetJsonAsync<List<achievement>>();

            for (var i = 0; i < UserData.Count; i++)
            {
                var achievementLabel = new Label
                {
                    Text = UserData[i].Achievement1,
                    FontSize = 20,
                    VerticalOptions = LayoutOptions.Center
                };

                achievementLabel.SetValue(Grid.RowProperty, i + 2);
                achievementLabel.SetValue(Grid.ColumnProperty, 0);

                var achievementDelete = new Button
                {
                    Text = "X",
                    BackgroundColor = Color.Red,
                };

                achievementDelete.SetValue(Grid.RowProperty, i + 2);
                achievementDelete.SetValue(Grid.ColumnProperty, 1);
                achievementDelete.Clicked += Delete_Achievement_Button;
                achievementDelete.ClassId = UserData[i].Id.ToString();

                Achievements_Grid.Children.Add(achievementLabel);
                Achievements_Grid.Children.Add(achievementDelete);
            }
        }

        async void Delete_Achievement_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Delete Achievement", "Are you sure you want to delete this achievement?", "Yes", "No");

            if (response == true)
            {
                var button = (Button)sender;
                var ID = int.Parse(button.ClassId);
                var URL = await SecureStorage.GetAsync("URL");

                await URL.AppendPathSegment("Achievement").AppendPathSegment(ID).DeleteAsync();

                var row = Grid.GetRow(button);
                var children = Achievements_Grid.Children.ToList();

                foreach (var child in children.Where(child => Grid.GetRow(child) == row))
                    Achievements_Grid.Children.Remove(child);
                foreach (var child in children.Where(child => Grid.GetRow(child) > row))
                    Grid.SetRow(child, Grid.GetRow(child) - 1);

                await DisplayAlert("Done!", "Achievement has been deleted!", "Okay!");
            }
        }

        async void Add_Achievement_Button(object sender, System.EventArgs e)
        {
            int ID = int.Parse(Preferences.Get("PID", ""));
            var URL = await SecureStorage.GetAsync("URL");

            if (Add_Achievement_Input.Text != "")
            {
                await URL.AppendPathSegment("Achievement").PostJsonAsync(new { PatientID = ID, Achievement1 = Add_Achievement_Input.Text });
                await DisplayAlert("Done!", "Achievement has been added!", "Okay!");
                LoadInfo();
            }
            else
                await DisplayAlert("Error", "Achievement is empty and has not added", "Okay!");

            Add_Achievement_Input.Text = "";
        }

    }
}