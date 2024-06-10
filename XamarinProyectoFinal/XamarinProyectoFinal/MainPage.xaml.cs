using System;
using Xamarin.Forms;
using XamarinProyectoFinal.Database;
using XamarinProyectoFinal.Models;
using XamarinProyectoFinal.Views;

namespace XamarinProyectoFinal
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MainPage()
        {
            Title = "Login";
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            var user = await _databaseService.GetUserAsync(username, password);

            if (user != null)
            {
                // Navigate to another page
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                // Display error message
                errorLabel.Text = "Invalid username or password";
                errorFrame.IsVisible = true;
            }
        }

        private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
        {
            var username = usernameEntry.Text;
            var password = passwordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errorLabel.Text = "Username and password are required";
                errorLabel.IsVisible = true;
                return;
            }

            var user = new User { Username = username, Password = password };
            await _databaseService.SaveUserAsync(user);

            // Clear entries
            usernameEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;

            errorLabel.Text = "User created successfully";
            errorLabel.TextColor = Color.Green;
            errorLabel.IsVisible = true;
        }

        private void OnCloseErrorButtonClicked(object sender, EventArgs e)
        {
            errorFrame.IsVisible = false;
        }

    }
}
