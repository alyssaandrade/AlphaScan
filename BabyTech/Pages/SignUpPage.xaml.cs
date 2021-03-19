using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BabyTech.ViewModels;
using System.ComponentModel;

namespace BabyTech.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel ViewModel => BindingContext as SignUpViewModel;
        public SignUpPage()
        {
            InitializeComponent();
            BindingContextChanged += Page_BindingContextChanged;
            BindingContext = new SignUpViewModel(Navigation);

            void Page_BindingContextChanged(object sender, EventArgs e)
            {
                ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
            }

            void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
            {
                var propHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
                switch (e.PropertyName)
                {
                    case nameof(ViewModel.Username):
                        emailField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.FirstName):
                        firstNameField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.LastName):
                        lastNameField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.Password):
                        passwordField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.ConfirmPassword):
                        confirmPasswordField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    default:
                        break;

                }
            }
        }


        private async void SignIn_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage());
        }

    }
}