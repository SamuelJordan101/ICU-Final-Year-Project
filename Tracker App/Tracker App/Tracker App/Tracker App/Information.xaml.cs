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
using System.IO;
using Acr.UserDialogs;

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Information : ContentPage
    {
        public Information()
        {
            InitializeComponent();
            LoadInfo();
        }

        public class patientdata
        {
            public int patientId { get; set; }
            public string name { get; set; }
            public DateTime admission { get; set; }
            public string ward { get; set; }
            public string hospital { get; set; }
            public int? goalCpax { get; set; }
        }

        public class image
        {
            public int Id { get; set; }
            public byte[] ImageData { get; set; }
            public string Category { get; set; }
            public int? PatientID { get; set; }
        }

        async void LoadInfo()
        {
            UserDialogs.Instance.ShowLoading("Loading Information...");
            var ID = Preferences.Get("PID","");
            var URL = await SecureStorage.GetAsync("URL");

            List<patientdata> data = await URL.AppendPathSegment("Patient").AppendPathSegment(ID).GetJsonAsync<List<patientdata>>();

            Name.Text = "Name: " + data[0].name;
            Hospital.Text = "Medical Centre: " + data[0].hospital;
            Ward.Text = "Ward: " + data[0].ward;
            Date.Text = "Admission Date: " + (data[0].admission.ToString()).Substring(0, data[0].admission.ToString().Length-11);
            UserDialogs.Instance.HideLoading();

            List<image> HospitalImage = await URL.AppendPathSegment("Image").AppendPathSegment(ID).GetJsonAsync<List<image>>();

            Hospital_Image.Source = ImageSource.FromStream(() => new MemoryStream(HospitalImage[0].ImageData));
        }
    }
}