using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Flurl;
using Flurl.Http;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Goals : ContentPage
    {
        public Goals()
        {
            InitializeComponent();
            LoadInfo();
        }

        public class goal
        {
            public int? Id { get; set; }
            public int PatientId { get; set; }
            public string Goal1 { get; set; }
            public bool Assigned { get; set; }
            public DateTime? DueDate { get; set; }
            public bool? Done { get; set; }
        }

        async void LoadInfo()
        {
            UserDialogs.Instance.ShowLoading("Loading Goals...");
            var ID = Preferences.Get("PID", "");
            var URL = await SecureStorage.GetAsync("URL");

            List<goal> UserData = await URL.AppendPathSegment("Goal").AppendPathSegment(ID).AppendPathSegment(false).GetJsonAsync<List<goal>>();

            for (var i = 0; i < UserData.Count;i++)
            {
                if(UserData[i].Done == false)
                {
                    var goalLabel = new Label
                    {
                        Text = UserData[i].Goal1,
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.Center
                    };

                    goalLabel.SetValue(Grid.RowProperty, i + 2);
                    goalLabel.SetValue(Grid.ColumnProperty, 0);

                    var goalDone = new Button
                    {
                        Text = "Done",
                        BackgroundColor = Color.DeepSkyBlue,
                    };

                    goalDone.SetValue(Grid.RowProperty, i + 2);
                    goalDone.SetValue(Grid.ColumnProperty, 1);
                    goalDone.Clicked += Done_Goal_Button;
                    goalDone.ClassId = UserData[i].Id.ToString();
                    goalDone.StyleId = "false";

                    var goalDelete = new Button
                    {
                        Text = "X",
                        BackgroundColor = Color.Red,
                    };

                    goalDelete.SetValue(Grid.RowProperty, i + 2);
                    goalDelete.SetValue(Grid.ColumnProperty, 2);
                    goalDelete.Clicked += Delete_Goal_Button;
                    goalDelete.ClassId = UserData[i].Id.ToString();

                    User_Goals.Children.Add(goalLabel);
                    User_Goals.Children.Add(goalDone);
                    User_Goals.Children.Add(goalDelete);
                }
            }

            List<goal> HospitalData = await URL.AppendPathSegment("Goal").AppendPathSegment(ID).AppendPathSegment(true).GetJsonAsync<List<goal>>();

            for (var i = 0; i < HospitalData.Count; i++)
            {
                if (HospitalData[i].Done == false)
                {
                    var goalLabel = new Label
                    {
                        Text = HospitalData[i].Goal1,
                        FontSize = 20,
                        VerticalOptions = LayoutOptions.Center,
                    };

                    goalLabel.SetValue(Grid.RowProperty, (i * 2) + 1);
                    goalLabel.SetValue(Grid.ColumnProperty, 0);

                    var goalDone = new Button
                    {
                        Text = "Done",
                        BackgroundColor = Color.DeepSkyBlue,
                    };

                    goalDone.SetValue(Grid.RowProperty, (i * 2) + 1);
                    goalDone.SetValue(Grid.ColumnProperty, 1);
                    goalDone.Clicked += Done_Goal_Button;
                    goalDone.ClassId = HospitalData[i].Id.ToString();
                    goalDone.StyleId = "true";

                    var goalDate = new Label
                    {
                        Text = "Due: " + (HospitalData[i].DueDate.Value.Day + "/" + HospitalData[i].DueDate.Value.Month + "/" + HospitalData[i].DueDate.Value.Year),
                        FontSize = 20,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                    };

                    DateTime currentDate = DateTime.Now;

                    if (HospitalData[i].DueDate.Value.Year > currentDate.Year)
                        goalDate.BackgroundColor = Color.Lime;
                    else if (HospitalData[i].DueDate.Value.Year == currentDate.Year)
                    {
                        if (HospitalData[i].DueDate.Value.Month > currentDate.Month)
                            goalDate.BackgroundColor = Color.Lime;
                        else if (HospitalData[i].DueDate.Value.Month == currentDate.Month)
                        {
                            if (HospitalData[i].DueDate.Value.Day > currentDate.Day)
                                goalDate.BackgroundColor = Color.Lime;
                            else if (HospitalData[i].DueDate.Value.Day == currentDate.Day)
                                goalDate.BackgroundColor = Color.Orange;
                            else
                                goalDate.BackgroundColor = Color.Red;
                        }
                        else
                            goalDate.BackgroundColor = Color.Red;
                    }
                    else
                        goalDate.BackgroundColor = Color.Red;

                    goalDate.SetValue(Grid.RowProperty, (i*2) + 2);
                    goalDate.SetValue(Grid.ColumnSpanProperty, 2);

                    Hospital_Goals.Children.Add(goalLabel);
                    Hospital_Goals.Children.Add(goalDone);
                    Hospital_Goals.Children.Add(goalDate);
                }
            }
            UserDialogs.Instance.HideLoading();
        }

        async void Done_Goal_Button(object sender, System.EventArgs e)
        {
            var button = (Button)sender;
            var ID = int.Parse(button.ClassId);
            var URL = await SecureStorage.GetAsync("URL");

            await URL.AppendPathSegment("Goal").AppendPathSegment(ID).PutAsync();

            var HospitalSet = button.StyleId;
            var row = Grid.GetRow(button);

            if (HospitalSet == "true")
            {
                var children = Hospital_Goals.Children.ToList();
                foreach (var child in children.Where(child => Grid.GetRow(child) == row))
                    Hospital_Goals.Children.Remove(child);
                foreach (var child in children.Where(child => Grid.GetRow(child) == row + 1))
                    Hospital_Goals.Children.Remove(child);
                foreach (var child in children.Where(child => Grid.GetRow(child) > row))
                    Grid.SetRow(child, Grid.GetRow(child) - 2);
            }
            else
            {
                var children = User_Goals.Children.ToList();
                foreach (var child in children.Where(child => Grid.GetRow(child) == row))
                    User_Goals.Children.Remove(child);
                foreach (var child in children.Where(child => Grid.GetRow(child) > row))
                    Grid.SetRow(child, Grid.GetRow(child) - 1);
            }

            await DisplayAlert("Completed!", "Goal Has Been Completed!", "Okay!");
        }

        async void Delete_Goal_Button(object sender, System.EventArgs e)
        {
            bool response = await DisplayAlert("Delete Goal", "Are you sure you want to delete this goal?", "Yes", "No");

            if (response == true)
            {
                var button = (Button)sender;
                var ID = int.Parse(button.ClassId);
                var URL = await SecureStorage.GetAsync("URL");

                await URL.AppendPathSegment("Goal").AppendPathSegment(ID).DeleteAsync();

                var row = Grid.GetRow(button);
                var children = User_Goals.Children.ToList();

                foreach (var child in children.Where(child => Grid.GetRow(child) == row))
                    User_Goals.Children.Remove(child);
                foreach (var child in children.Where(child => Grid.GetRow(child) > row))
                    Grid.SetRow(child, Grid.GetRow(child) - 1);

                await DisplayAlert("Done!", "Goal has been deleted!", "Okay!");
            }
        }

        async void Add_Goal_Button(object sender, System.EventArgs e)
        {
            int ID = int.Parse(Preferences.Get("PID", ""));
            var URL = await SecureStorage.GetAsync("URL");

            if(Add_Goal_Input.Text != "")
            {
                await URL.AppendPathSegment("Goal").PostJsonAsync(new { PatientId = ID, Goal1 = Add_Goal_Input.Text, Assigned = false, Done = false });
                await DisplayAlert("Done!", "Goal has been added!", "Okay!");
                LoadInfo();
            }
            else
                await DisplayAlert("Error", "Goal is empty and has not added", "Okay!");

            Add_Goal_Input.Text = "";
        }


    }
}