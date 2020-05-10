using Acr.UserDialogs;
using SDSD.Bootstrap;
using SDSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SDSD.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            NavigateToMainPageCommand = new Command(async () => await ExecuteNavigateToMainPageCommand());
        }
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private async Task ExecuteNavigateToMainPageCommand()
        {
            if (string.IsNullOrEmpty(Username))
            {
                await DisplayAlert("Information", "Enter the Username!", "OK");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Information", "Enter the Password!", "OK");
                return;
            }
            if (IsAuthenticated(Username, Password))
                    App.Current.MainPage = new MainPage();
            else
                await DisplayAlert("Information", "Invalid Credentials!", "OK");
        }

        public Command NavigateToMainPageCommand { get; }
        private bool IsAuthenticated(string username, string password)
        {
            using (UserDialogs.Instance.Loading("one moment please...", null, null, true, MaskType.Black))
            {
                var response = users.Where(c => c.userName == username && c.password == password).SingleOrDefault();
                if (response != null)
                {
                    Settings.Username = response.userName;
                    return true;
                }
                else
                    return false;
            }
               
        }
        List<User> users = new List<User>()
        {
            new User
            {
                userName ="admin",
                password = "admin"
            },
            new User
            {
                userName ="sdsd",
                password = "password"
            },
        };
    }
}
