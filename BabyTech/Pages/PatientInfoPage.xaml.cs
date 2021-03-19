using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using BabyTech.ViewModels;

namespace BabyTech.Pages
{
    public partial class PatientInfoPage : ContentPage
    {
        protected override void OnAppearing()
        {
            string[] genderoptions = { "male", "female", "other" };
            var genders = new List<string>(genderoptions);
            GenderPicker.ItemsSource = genderoptions;
            
        }

        PatientInfoViewModel ViewModel => BindingContext as PatientInfoViewModel;

        public PatientInfoPage()
        {
            InitializeComponent();
            BindingContextChanged += Page_BindingContextChanged;
            BindingContext = new PatientInfoViewModel(Navigation);

            void Page_BindingContextChanged(object sender, EventArgs e)
            {
                ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
            }

            void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
            {
                var propHasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
                switch (e.PropertyName)
                {
                    case nameof(ViewModel.FirstName):
                        firstNameField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    case nameof(ViewModel.LastName):
                        lastNameField.TextColor = propHasErrors ? Color.Red : Color.Black;
                        break;
                    default:
                        break;

                }
            }
            
        }
    }
}
