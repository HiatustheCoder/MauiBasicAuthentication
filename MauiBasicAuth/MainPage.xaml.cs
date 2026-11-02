
using MauiBasicAuth.DataAccess;

namespace MauiBasicAuth
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = txtUserId.Text ?? "";
            string password = txtPassword.Text ?? "";

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error",
                    "Please enter your username and password.", "OK");
                return;
            }

            var userAuth = new UserAuthentication();

            try
            {
                bool isAuthenticated =
                    await userAuth.AuthenticateUser(username, password);

                if (isAuthenticated)
                {
                    await DisplayAlert("Success",
                        "User authenticated successfully!", "OK");
                }
                else
                {
                    await DisplayAlert("Error",
                        "Invalid username or password.", "OK");
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Connection Error",
                    "Unable to connect to the authentication API.", "OK");
            }
            catch (TaskCanceledException)
            {
                await DisplayAlert("Connection Error",
                    "The API request timed out.", "OK");
            }
        }
    }
}
