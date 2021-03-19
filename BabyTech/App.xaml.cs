using BabyTech.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Amazon.CognitoIdentity;
using Newtonsoft.Json;

namespace BabyTech
{
    public partial class App : Application
    {
        public static AWSUser user;
        public static AmazonCognitoIdentityProviderClient provider;

        public App()
        {
            InitializeComponent();

            provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), Amazon.RegionEndpoint.USEast1);

            MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            string secureUser = await SecureStorage.GetAsync("User");
            if (secureUser != null)
            {
                user = JsonConvert.DeserializeObject<AWSUser>(secureUser);
            }
            else
            {
                user = null;
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
