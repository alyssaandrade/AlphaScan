using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using BabyTech.Services;
using Newtonsoft.Json;

namespace BabyTech.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLogin;
        public Action DisplayCorrectLogin;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string password;
        public string Password
        {
            get => password;

            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public ICommand SubmitCommand
        {
            protected set; get;
        }

        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        public async void OnSubmit() 
        {
            String result = await Auth.SignIn(email, password);
            if (!result.StartsWith("Error"))
            {
                await SecureStorage.SetAsync("User", JsonConvert.SerializeObject(App.user));
                DisplayCorrectLogin();
            }
            else
            {
                Console.WriteLine(result);
                DisplayInvalidLogin();
            }
        }

    }

}
