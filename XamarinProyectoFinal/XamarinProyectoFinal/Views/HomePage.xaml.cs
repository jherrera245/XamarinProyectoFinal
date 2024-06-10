using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProyectoFinal.Database;
using XamarinProyectoFinal.Models;

namespace XamarinProyectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private Mood _mood;
        private DatabaseService _databaseService;

        public HomePage()
        {
            Title = "Inicio";
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CalculateAggregateFunctions();
        }

        private async void CalculateAggregateFunctions()
        {
            // Recuperar todos los registros de la tabla Mood
            List<Mood> moods = await _databaseService.GetMoodsAsync();

            // Calcular funciones de agregado
            int totalMoods = moods.Count;
            double averageMood = moods.Select(m => MoodToInt(m.Status)).Average();
            int happyMoods = moods.Count(m => m.Status == "Feliz");
            int sadMoods = moods.Count(m => m.Status == "Triste");
            int neutralMoods = moods.Count(m => m.Status == "Estresado");
            int calmMoods = moods.Count(m => m.Status == "Calmado");

            lblAverageMoodFrame.Text = $"{averageMood:F2}";
            lblHappyMoodsFrame.Text = $"{happyMoods}";
            lblSadMoodsFrame.Text = $"{sadMoods}";
            lblNeutralMoodsFrame.Text = $"{(neutralMoods + calmMoods):F2}";
        }

        public static int MoodToInt(string mood)
        {
            switch (mood)
            {
                case "Feliz":
                    return 3;
                case "Triste":
                    return 2;
                case "Estresado":
                    return 1;
                case "Calmado":
                    return 0;
                default:
                    return -1; // Valor de error, si no hay coincidencia
            }
        }

        private async void OnMoodTrackingButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoodPage());
        }

        private async void OnResourcesButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResourcePage());
        }

        private async void OnRemindersButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReminderPage());
        }
    }
}