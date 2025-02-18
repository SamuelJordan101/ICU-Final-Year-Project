﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Flurl;
using Flurl.Http;
using Acr.UserDialogs;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        public Login()
        {
            InitializeComponent();
        }

        public class patient
        {
            public int PatientId { get; set; }
            public string Name { get; set; }
            public DateTime Admission { get; set; }
            public string Ward { get; set; }
            public string Hospital { get; set; }
            public int? GoalCpax { get; set; }
        }

        async void Login_Button(object sender, System.EventArgs e)
        {
            
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                var ID = EnterID.Text;

                if (ID != null && ValidateAmount() == true)
                {
                    UserDialogs.Instance.ShowLoading("Logging In...");
                    try {
                        var URL = await SecureStorage.GetAsync("URL");
                        List<patient> dataLogin = await URL.AppendPathSegment("Patient").AppendPathSegment("Check").AppendPathSegment(ID).GetJsonAsync<List<patient>>();
                        if (dataLogin[0].PatientId == int.Parse(ID))
                        {
                            Preferences.Set("PID", ID);
                            UserDialogs.Instance.HideLoading();
                            await Navigation.PushModalAsync(new Tracker_App.Home());
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Incorrect Login!", "Please Try Again!", "OK");
                            EnterID.Text = "";
                        }
                    } catch
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Please Try Again!", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Entry!", "Please Enter a valid ID", "OK");
                    EnterID.Text = "";
                }
            } else
            {
                EnterID.Text = "";
                await DisplayAlert("No Internet Access!", "You currently have no internet access. Please connect to the internet and try again", "OK!");
            }
        }

        private bool ValidateAmount()
        {
            if (int.TryParse(EnterID.Text, out int result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override bool OnBackButtonPressed() => false;

    }
}