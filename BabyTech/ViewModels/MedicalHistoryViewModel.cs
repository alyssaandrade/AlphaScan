using BabyTech.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BabyTech.Models;
using BabyTech.Services;

namespace BabyTech.ViewModels
{
    public class MedicalHistoryViewModel : BaseValidationViewModel
    {
        public INavigation Navigation { get; set; }

        private Patient patient;

        private bool _birthComplications = false;
        public bool BirthComplications
        {
            get => _birthComplications;
            set => _birthComplications = value;
        }

        private bool _geneticDisorders = false;
        public bool GeneticDisorders
        {
            get => _geneticDisorders;
            set => _geneticDisorders = value;
        }

        private bool _chromosomalAbnormalities = false;
        public bool ChromosomalAbnormalities
        {
            get => _chromosomalAbnormalities;
            set => _chromosomalAbnormalities = value;
        }

        private bool _siblingMedicalIssues = false;
        public bool SiblingMedicalIssues
        {
            get => _siblingMedicalIssues;
            set => _siblingMedicalIssues = value;
        }

        private bool _familyMedicalIssues = false;
        public bool FamilyMedicalIssues
        {
            get => _familyMedicalIssues;
            set => _familyMedicalIssues = value;
        }

        private bool _surgeryHistory = false;
        public bool SurgeryHistory
        {
            get => _surgeryHistory;
            set => _surgeryHistory = value;
        }

        private bool _allergies = false;
        public bool Allergies
        {
            get => _allergies;
            set => _allergies = value;
        }

        private bool _motherOver35 = false;
        public bool MotherOver35
        {
            get => _motherOver35;
            set => _motherOver35 = value;
        }

        private bool _fatherOver40 = false;
        public bool FatherOver40
        {
            get => _fatherOver40;
            set => _fatherOver40 = value;
        }

        private bool _previousMiscarraige = false;
        public bool PreviousMiscarraige
        {
            get => _previousMiscarraige;
            set => _previousMiscarraige = value;
        }

        private bool _exposure = false;
        public bool Exposure
        {
            get => _exposure;
            set => _exposure = value;
        }

        private bool _drugsOrAlcohol = false;
        public bool DrugsOrAlcohol
        {
            get => _drugsOrAlcohol;
            set => _drugsOrAlcohol = value;
        }

        private bool _diabetes = false;
        public bool Diabetes
        {
            get => _diabetes;
            set => _diabetes = value;
        }

        private bool _accutaneUse = false;
        public bool AccutaneUse
        {
            get => _accutaneUse;
            set => _accutaneUse = value;
        }

        // Command linked to "Next" button
        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save));

        async void Save()
        {
            // Update model with data from form
            patient.BirthComplications = _birthComplications;
            patient.GeneticDisorders = _geneticDisorders;
            patient.ChromosomalAbnormalities = _chromosomalAbnormalities;
            patient.SiblingMedicalIssues = _siblingMedicalIssues;
            patient.FamilyMedicalIssues = _familyMedicalIssues;
            patient.SurgeryHistory = _surgeryHistory;
            patient.Allergies = _allergies;
            patient.MotherOver35 = _motherOver35;
            patient.FatherOver40 = _fatherOver40;
            patient.PreviousMiscarraige = _previousMiscarraige;
            patient.Exposure = _exposure;
            patient.DrugsOrAlcohol = _drugsOrAlcohol;
            patient.Diabetes = _diabetes;
            patient.AccutaneUse = _accutaneUse;

            // Update patient record in DB
            DB db = new DB();
            await db.UpdatePatient(patient);
            
            // Send user to photo capture page
            await Navigation.PushAsync(new Instructions(patient));
        }

        public MedicalHistoryViewModel(INavigation navigation, Patient patient)
        {
            // Allow viewModel to control navigation
            this.Navigation = navigation;

            // set the current patient
            this.patient = patient;
        }
    }
}
