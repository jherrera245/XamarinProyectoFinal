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
    public partial class ReminderPage : ContentPage
    {

        private DatabaseService _databaseService;
        private List<Reminder> _reminder;

        public ReminderPage()
        {
            Title = "Recordatorios";
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _reminder = await _databaseService.GetRemindersAsync();
            reminderListView.ItemsSource = _reminder;
        }


        public async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReminderFormPage());
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var selectedReminder = e.SelectedItem as Reminder;
                if (_reminder != null && _reminder.Contains(selectedReminder))
                {
                    var action = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Editar", "Eliminar");

                    switch (action)
                    {
                        case "Editar":
                            await Navigation.PushAsync(new ReminderFormPage(selectedReminder));
                            break;

                        case "Eliminar":
                            var confirmDelete = await DisplayAlert("Eliminar", "¿Estás seguro que deseas eliminar este estado de ánimo?", "Sí", "No");
                            if (confirmDelete)
                            {
                                _reminder.Remove(selectedReminder);
                                await _databaseService.DeleteReminderAsync(selectedReminder);
                            }
                            break;
                    }

                    ((ListView)sender).SelectedItem = null;
                }
            }
        }

    }
}