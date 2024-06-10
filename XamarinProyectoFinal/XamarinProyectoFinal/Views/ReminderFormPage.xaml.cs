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
    public partial class ReminderFormPage : ContentPage
    {
        private Reminder _reminder;
        private DatabaseService _databaseService;

        public ReminderFormPage()
        {
            InitializeUI(null);
        }

        public ReminderFormPage(Reminder reminder)
        {
            InitializeUI(reminder);
        }

        private void InitializeUI(Reminder reminder)
        {
            Title = reminder == null ? "Agregar Recordatorio" : "Editar Recordatorio";

            InitializeComponent();

            if (reminder != null)
            {

                timePicker.Time = reminder.Time;
                messageEntry.Text = reminder.Message;
                _reminder = reminder;
            }
            else
            {
                _reminder = new Reminder();
            }

            _databaseService = new DatabaseService();
        }

        private async void OnSaveReminderClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, introduce un mensaje", "OK");
                return;
            }

            _reminder.Time = timePicker.Time;
            _reminder.Message = messageEntry.Text;


            await _databaseService.SaveReminderAsync(_reminder);
            await Navigation.PopAsync();
        }
    }
}