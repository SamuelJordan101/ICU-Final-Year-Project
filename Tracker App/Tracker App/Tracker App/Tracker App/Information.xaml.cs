using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Flurl;
using Flurl.Http;

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

        async void LoadInfo()
        {
            var ID = Preferences.Get("PID","");
            List<patientdata> data = await "http://10.0.2.2/Tracker.API/Patient/".AppendPathSegment(ID).GetJsonAsync<List<patientdata>>();

            Name.Text = "Name: " + data[0].name;
            Hospital.Text = "Medical Centre: " + data[0].hospital;
            Ward.Text = "Ward: " + data[0].ward;
            Date.Text = "Admission Date: " + (data[0].admission.ToString()).Substring(0, data[0].admission.ToString().Length-11);
        }
    }
}