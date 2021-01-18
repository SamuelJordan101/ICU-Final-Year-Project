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
    public partial class Login : ContentPage
    {

        public Login()
        {
            InitializeComponent();
        }

        void Login_Button(object sender, System.EventArgs e)
        {
            var ID = EnterID.Text;

            if (ID != null && ValidateAmount() == true)
            {
                Navigation.PushModalAsync(new Tracker_App.Home());
            } else {
                DisplayAlert("Invalid Entry!", "Please Enter a valid ID", "OK");
                EnterID.Text = "";
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

    }
}