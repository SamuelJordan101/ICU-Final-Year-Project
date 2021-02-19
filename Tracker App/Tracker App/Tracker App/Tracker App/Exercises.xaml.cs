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

namespace Tracker_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Exercises : ContentPage
    {
        public Exercises()
        {
            InitializeComponent();
            LoadExercises();
        }

        public class exercise
        {
            public int Id { get; set; }
            public string ExerciseName { get; set; }
            public string Category { get; set; }
            public int StepId { get; set; }
            public int Image { get; set; }
        }

        public class image
        {
            public int Id { get; set; }
            public byte[] ImageData { get; set; }
            public string Category { get; set; }
            public int? PatientID { get; set; }
        }

        async void LoadExercises()
        {
            List<exercise> ExerciseData = await "http://10.0.2.2/Tracker.API/Exercise".GetJsonAsync<List<exercise>>();

            for(var i = 0; i < ExerciseData.Count; i++)
            {
                List<image> Image = await "http://10.0.2.2/Tracker.API/Image/".AppendPathSegment(i).GetJsonAsync<List<image>>();

                var ExerciseGrid = new Grid
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };

                ExerciseGrid.RowDefinitions.Add(new RowDefinition { Height=80 });
                ExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) });
                ExerciseGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });

                var ExerciseFrame = new Frame
                {
                    Content = ExerciseGrid,
                    CornerRadius = 5,
                    Margin = 10,
                };

                var ExerciseLabel = new Label
                {
                    Text = ExerciseData[i].ExerciseName,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20
                };

                var ExerciseImage = new Image
                {
                    VerticalOptions = LayoutOptions.Fill,
                    Aspect = Aspect.AspectFit
                };

                ExerciseImage.Source = ImageSource.FromStream(() => new MemoryStream(Image[i].ImageData));
                ExerciseImage.SetValue(Grid.ColumnProperty, 1);

                var ExerciseButton = new Button
                {
                    Text = "View",
                    BackgroundColor = Color.Pink,
                    VerticalOptions = LayoutOptions.Fill
                };

                ExerciseButton.SetValue(Grid.RowProperty, 1);
                ExerciseButton.SetValue(Grid.ColumnSpanProperty, 2);
                ExerciseButton.Clicked += Exercise_Button;
                ExerciseButton.ClassId = i.ToString();

                ExerciseGrid.Children.Add(ExerciseLabel);
                ExerciseGrid.Children.Add(ExerciseImage);
                ExerciseGrid.Children.Add(ExerciseButton);

                ExerciseLayout.Children.Add(ExerciseFrame);
            }
        }

        void Exercise_Button(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Tracker_App.Exercise_Individual());
        }
    }
}