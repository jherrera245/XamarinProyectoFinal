using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProyectoFinal.Database;
using XamarinProyectoFinal.Models;

namespace XamarinProyectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoodPage : ContentPage
    {
        private DatabaseService _databaseService;
        private List<Mood> _moods;

        public MoodPage()
        {
            Title = "Estados de animo";
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _moods = await _databaseService.GetMoodsAsync();
            moodListView.ItemsSource = _moods;
        }

        public async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MoodFormPage());
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var selectedMood = e.SelectedItem as Mood;
                if (_moods != null && _moods.Contains(selectedMood))
                {
                    var action = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Editar", "Eliminar");

                    switch (action)
                    {
                        case "Editar":
                            await Navigation.PushAsync(new MoodFormPage(selectedMood));
                            break;

                        case "Eliminar":
                            var confirmDelete = await DisplayAlert("Eliminar", "¿Estás seguro que deseas eliminar este estado de ánimo?", "Sí", "No");
                            if (confirmDelete)
                            {
                                _moods.Remove(selectedMood);
                                await _databaseService.DeleteMoodAsync(selectedMood);
                            }
                            break;
                    }

                    ((ListView)sender).SelectedItem = null;
                }
            }
        }

    }
}
