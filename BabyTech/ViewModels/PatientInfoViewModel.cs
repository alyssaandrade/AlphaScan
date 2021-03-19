using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BabyTech.Models;
using BabyTech.Pages;
using BabyTech.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BabyTech.ViewModels
{
    public class PatientInfoViewModel : BaseValidationViewModel
    {

        public INavigation Navigation { get; set; }

        private Guid _patientID;
        public string PatientID
        {
            get => _patientID.ToString();
        }

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;

            set
            {
                _firstName = value;

                // validate to ensure first name is not empty before saving
                Validate(() => !string.IsNullOrWhiteSpace(_firstName), "First name is required.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;

            set
            {
                _lastName = value;

                // validate to ensure last name is not empty before saving
                Validate(() => !string.IsNullOrWhiteSpace(_lastName), "Last name is required.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private string _gender = string.Empty;
        public string Gender
        {
            get => _gender;
            set => _gender = value;
        }

        private string _streetAddress = string.Empty;
        public string StreetAddress
        {
            get => _streetAddress;
            set => _streetAddress = value;
        }

        private string _state = string.Empty;
        public string State
        {
            get => _state;
            set => _state = value;
        }

        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set => _city = value;
        }

        private string _country = string.Empty;
        public string Country
        {
            get => _country;
            set => _country = value;
        }

        private string _hospitalName = string.Empty;
        public string HospitalName
        {
            get => _hospitalName;
            set => _hospitalName = value;
        }

        private string _observations = string.Empty;
        public string Observations
        {
            get => _observations;
            set => _observations = value;
        }

        // Command linked to "Register Case" button
        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));
        async void Save()
        {
            // Create connection to database
            DB db = new DB();

            // Create a new patient object
            Patient newPatient = new Patient()
            {
                PatientID = _patientID.ToString(),
                FirstName = _firstName,
                LastName = _lastName,
                Gender = _gender,
                Country = _country,
                State = _state,
                City = _city,
                HospitalName = _hospitalName,
                Observations = _observations
            };

            // Add patient to database
            await db.CreatePatient(newPatient);

            // Navigate to next page
            await Navigation.PushAsync(new MedicalHistoryPage(newPatient));
        }
        bool CanSave() => !string.IsNullOrWhiteSpace(_firstName) && !string.IsNullOrWhiteSpace(_lastName) && !HasErrors;

        public PatientInfoViewModel(INavigation navigation)
        {
            // Create a new UUID for the patient when initializing the viewmodel
            _patientID = System.Guid.NewGuid();

            // Allow viewModel to control navigation
            this.Navigation = navigation;
        }
    }
}
