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
    public partial class Exercise_Individual : ContentPage
    {
        public Exercise_Individual()
        {
            InitializeComponent();
            LoadExercise();
        }

        public class step
        {
            public int Id { get; set; }
            public string Step1 { get; set; }
            public int Image { get; set; }
            public int ExerciseID { get; set; }
        }
        public class exercise
        {
            public int Id { get; set; }
            public string ExerciseName { get; set; }
            public string ExerciseDescription { get; set; }
            public int Image { get; set; }
            public int Gif { get; set; }
        }
        public class image
        {
            public int Id { get; set; }
            public byte[] ImageData { get; set; }
            public string Category { get; set; }
            public int? PatientID { get; set; }
        }

        async void LoadExercise()
        {
            UserDialogs.Instance.ShowLoading("Loading Steps...");
            var Exercise = Preferences.Get("Exercise",1);
            var URL = await SecureStorage.GetAsync("URL");
            List<step> Steps = new List<step>();


            try { Steps = await URL.AppendPathSegment("Step").AppendPathSegment(Exercise).GetJsonAsync<List<step>>(); } catch { }
            List<exercise> ExerciseData = await URL.AppendPathSegment("Exercise").AppendPathSegment(Exercise).GetJsonAsync<List<exercise>>();
            List<image> ExerciseImage = await URL.AppendPathSegment("Image").AppendPathSegment("Exercise").AppendPathSegment(ExerciseData[0].Image).GetJsonAsync<List<image>>();
            List<image> ExerciseGif = await URL.AppendPathSegment("Image").AppendPathSegment("Exercise").AppendPathSegment(ExerciseData[0].Gif).GetJsonAsync<List<image>>();

            Exercise_Name.Text = ExerciseData[0].ExerciseName;
            Exercise_Description.Text = ExerciseData[0].ExerciseDescription;
            Gif.Source = ImageSource.FromStream(() => new MemoryStream(ExerciseGif[0].ImageData));
            Gif.WidthRequest = GifLayout.Width - 50;

            for (var i = 0; i < Steps.Count; i++)
            {
                List<image> StepIndividualImage = await URL.AppendPathSegment("Image").AppendPathSegment("Exercise").AppendPathSegment(Steps[i].Image).GetJsonAsync<List<image>>();

                var StepGrid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                StepGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.6, GridUnitType.Star) });
                StepGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.4, GridUnitType.Star) });
                StepGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) });

                var StepFrame = new Frame
                {
                    Content = StepGrid,
                    CornerRadius = 5,
                    Padding = 5
                };

                StepFrame.Margin = new Thickness(10,5,10,5);

                var StepLabel = new Label
                {
                    Text = "Step " + (i+1),
                    FontSize = 25
                };

                var StepDescription = new Label
                {
                    Text = Steps[i].Step1,
                };

                StepDescription.SetValue(Grid.RowProperty, 1);

                var StepImage = new Image
                {
                    Aspect = Aspect.AspectFill
                };

                StepImage.Source = ImageSource.FromStream(() => new MemoryStream(StepIndividualImage[0].ImageData));
                StepImage.SetValue(Grid.ColumnProperty, 1);
                StepImage.SetValue(Grid.RowSpanProperty, 2);

                StepGrid.Children.Add(StepLabel);
                StepGrid.Children.Add(StepDescription);
                StepGrid.Children.Add(StepImage);

                StepLayout.Children.Add(StepFrame);
            }
            UserDialogs.Instance.HideLoading();
        }
    }
}