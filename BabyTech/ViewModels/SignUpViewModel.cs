using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BabyTech.Services;
using BabyTech.Pages;


namespace BabyTech.ViewModels
{
    public class SignUpViewModel : BaseValidationViewModel
    {
        public INavigation navigation { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                Validate(() => !string.IsNullOrWhiteSpace(_username), "Username is required.");
                OnPropertyChanged();
                SignUpCommand.ChangeCanExecute();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Validate(() => !string.IsNullOrWhiteSpace(_password), "Please enter a password.");
                OnPropertyChanged();
                SignUpCommand.ChangeCanExecute();
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                Validate(() => !string.IsNullOrWhiteSpace(_confirmPassword), "Please confirm your password.");
                OnPropertyChanged();
                SignUpCommand.ChangeCanExecute();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        Command _signUpCommand;
        public Command SignUpCommand => _signUpCommand ?? (_signUpCommand = new Command(SignUp, CanSignUp));
        async void SignUp()
        {
            string result = await Auth.SignUp(_username, _password, _firstName, _lastName);
            if (!result.StartsWith("Error"))
            {
                Console.WriteLine(result);
            } 
            else 
            {
                await navigation.PushAsync(new LoginPage());    
            }
            Console.WriteLine(result);
        }

        bool CanSignUp() => !string.IsNullOrWhiteSpace(_username) 
            && !string.IsNullOrWhiteSpace(_password) 
            && !string.IsNullOrWhiteSpace(_firstName) 
            && !string.IsNullOrWhiteSpace(_lastName)
            && (_password == _confirmPassword)
            && !HasErrors;
    
        public SignUpViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
