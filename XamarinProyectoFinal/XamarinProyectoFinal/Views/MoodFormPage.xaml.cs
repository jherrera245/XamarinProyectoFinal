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
    public partial class MoodFormPage : ContentPage
    {
        private Mood _mood;
        private DatabaseService _databaseService;

        public MoodFormPage()
        {
            InitializeUI(null);
        }

        public MoodFormPage(Mood mood)
        {
            InitializeUI(mood);
        }

        private void InitializeUI(Mood mood)
        {
            Title = mood == null ? "Agregar Estado" : "Editar Estado";

            InitializeComponent();

            if (mood != null)
            {
                moodPicker.SelectedItem = mood.Status;
                notesEditor.Text = mood.Notes;
                _mood = mood;
            }
            else
            {
                _mood = new Mood();
            }

            _databaseService = new DatabaseService();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(notesEditor.Text))
            {
                await DisplayAlert("Error", "Por favor, introduce un estado", "OK");
                return;
            }
            
            _mood.Status = (string) moodPicker.SelectedItem;
            _mood.Notes = notesEditor.Text;

            await _databaseService.SaveMoodAsync(_mood);
            await Navigation.PopAsync();
        }
    }
}