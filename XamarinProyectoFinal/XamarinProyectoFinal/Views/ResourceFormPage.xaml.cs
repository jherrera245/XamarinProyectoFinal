using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using XamarinProyectoFinal.Database;
using XamarinProyectoFinal.Models;
using static Xamarin.Essentials.Permissions;

namespace XamarinProyectoFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourceFormPage : ContentPage
    {

        private Resource _resource;
        private DatabaseService _databaseService;
        private MediaFile _selectedImageFile;

        public ResourceFormPage()
        {
            InitializeUI(null);
        }

        public ResourceFormPage(Resource resource)
        {
            InitializeUI(resource);
        }

        private void InitializeUI(Resource resource)
        {
            Title = resource == null ? "Agregar Recurso" : "Editar Recurso";
            InitializeComponent();

            if (resource != null)
            {
                TitleEntry.Text = resource.Title;
                ContentEditor.Text = resource.Content;
                ImagePreview.Source = resource.ImageSource;
                _resource = resource;
            }
            else
            {
                _resource = new Resource();
            }

            _databaseService = new DatabaseService();
        }

        private async void OnPickImageClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "Error al seleccionar la imagen.", "OK");
                return;
            }

            _selectedImageFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            if (_selectedImageFile == null)
                return;

            ImagePreview.Source = ImageSource.FromStream(() =>
            {
                var stream = _selectedImageFile.GetStream();
                return stream;
            });
        }

        private byte[] ConvertImageToByteArray(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private async void OnSaveReourceClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(ContentEditor.Text))
            {
                await DisplayAlert("Error", "Por favor, introduce un titulo", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ContentEditor.Text))
            {
                await DisplayAlert("Error", "Por favor, introduce una descripcion", "OK");
                return;
            }

            if (_selectedImageFile == null)
            {
                await DisplayAlert("Error", "Por favor, seleccione una imagen", "OK");
                return;
            }


            _resource.Title = TitleEntry.Text;
            _resource.Content = ContentEditor.Text;

            if (_resource.Image == null || _selectedImageFile != null)
            {
                _resource.Image = ConvertImageToByteArray(_selectedImageFile.GetStream());
            }

            await _databaseService.SaveResourceAsync(_resource);
            await Navigation.PopAsync();
        }
    }
}