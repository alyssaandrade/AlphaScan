using BabyTech.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using BabyTech.Models;

namespace BabyTech.Pages
{
    public partial class MedicalHistoryPage : ContentPage
    {
        MedicalHistoryViewModel ViewModel => BindingContext as MedicalHistoryViewModel;
        public MedicalHistoryPage(Patient patient)
        {
            InitializeComponent();
            BindingContext = new MedicalHistoryViewModel(Navigation, patient);
        }

    }
}
