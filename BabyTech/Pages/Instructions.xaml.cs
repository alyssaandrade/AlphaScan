using System;
using System.Collections.Generic;

using Xamarin.Forms;
using BabyTech.Models;

namespace BabyTech.Pages
{
    public partial class Instructions : ContentPage
    {
        private Patient patient;

        public Instructions(Patient _patient)

        {
            this.patient = _patient;

            InitializeComponent();
        }

        private async void TakePhoto_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new TakePhoto(patient));
        }
    }
}
