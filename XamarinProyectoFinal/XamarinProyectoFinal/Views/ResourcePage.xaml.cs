using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinProyectoFinal.Database;
using XamarinProyectoFinal.Models;
using static Xamarin.Essentials.Permissions;

namespace XamarinProyectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcePage : ContentPage
    {
        private DatabaseService _databaseService;
        private List<Resource> _resource;

        public ResourcePage()
        {
            Title = "Recursos";
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _resource = await _databaseService.GetResourcesAsync();
            resourceListView.ItemsSource = _resource;
        }

        public async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResourceFormPage());
        }

        public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem != null)
            {
                var selectedResource = e.SelectedItem as Resource;
                if (_resource != null && _resource.Contains(selectedResource))
                {
                    var action = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Editar", "Eliminar");

                    switch (action)
                    {
                        case "Editar":
                            await Navigation.PushAsync(new ResourceFormPage(selectedResource));
                            break;

                        case "Eliminar":
                            var confirmDelete = await DisplayAlert("Eliminar", "¿Estás seguro que deseas eliminar este estado de ánimo?", "Sí", "No");
                            if (confirmDelete)
                            {
                                _resource.Remove(selectedResource);
                                await _databaseService.DeleteResourceAsync(selectedResource);
                            }
                            break;
                    }

                    ((ListView)sender).SelectedItem = null;
                }
            }
        }

    }
}