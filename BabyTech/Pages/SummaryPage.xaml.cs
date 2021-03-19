using System;
using System.Collections.Generic;

using Xamarin.Forms;
using BabyTech.Models;

namespace BabyTech.Pages
{
    public partial class SummaryPage : ContentPage
    {
        private Patient patient;
        private string PhotoPath;

        public SummaryPage(Patient _patient, string PhotoPath)
        {
            this.patient = _patient;

            this.PhotoPath = PhotoPath;

            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            resultImage.Source = ImageSource.FromFile(PhotoPath);
            

            Name.Text = "Name: " +patient.FirstName + " " + patient.LastName;
            PatientID.Text = "Patient ID: " + patient.PatientID;
            Gender.Text = "Gender: " + patient.Gender;
            Country.Text = "Country: " + patient.Country;
            State.Text = "State: " + patient.State;
            City.Text = "City: " + patient.City;
            HospitalName.Text = "Hospital: " + patient.HospitalName;
            Observations.Text = "Observations: " + patient.Observations;
            BirthComplicationsLabel.Text = "Complications during birth? " + (patient.BirthComplications ? "✔" : "❌");
            GeneticDisordersLabel.Text = "Family history of genetic disorders " + (patient.GeneticDisorders ? "✔" : "❌");
            ChromosomalAbnormalitiesLabel.Text = "Family history of chromosomal abnormalities " + (patient.ChromosomalAbnormalities ? "✔" : "❌");
            SiblingMedicalIssuesLabel.Text = "Medical issues among family " + (patient.SiblingMedicalIssues ? "✔" : "❌");
            FamilyMedicalLabel.Text = "Medical issues among family " + (patient.FamilyMedicalIssues ? "✔" : "❌");
            SurgeryHistoryLabel.Text = "History of surgeries " + (patient.SurgeryHistory ? "✔" : "❌");
            AllergiesLabel.Text = "Child has allergies " + (patient.Allergies ? "✔" : "❌");
            MotherOver35Label.Text = "Mother over the age of 35 " + (patient.MotherOver35 ? "✔" : "❌");
            FatherOver40Label.Text = "Father over the age of 40 " + (patient.FatherOver40 ? "✔" : "❌");
            PreviousMiscarraigeLabel.Text = "Previous history of miscarraige " + (patient.PreviousMiscarraige ? "✔" : "❌");
            ExposureLabel.Text = "Exposure to toxic chemicals/ x-rays " + (patient.Exposure ? "✔" : "❌");
            DrugsOrAlcoholLabel.Text = "Drug or alcohol history " + (patient.DrugsOrAlcohol ? "✔" : "❌");
            DiabetesLabel.Text = "Diabetes during pregnancy " + (patient.Diabetes ? "✔" : "❌");
            AccutaneUseLabel.Text = "Has taken isotretinoin or accutane " + (patient.AccutaneUse ? "✔" : "❌");
           









        }
    }

}
