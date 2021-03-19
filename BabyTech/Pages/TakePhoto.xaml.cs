using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Amazon.CognitoIdentity;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System.IO;
using BabyTech.Models;
using BabyTech.Services;
using Amazon;

namespace BabyTech.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePhoto : ContentPage
    {

        private Patient patient;

        private string facialAnalysisData;
        public string FacialAnalysisData
        {
            get { return facialAnalysisData;  }
            set
            {
                facialAnalysisData = value;
                OnPropertyChanged(nameof(FacialAnalysisData));
            }
        }

        public string PhotoPath { get; private set; }

        public TakePhoto(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
        }

        async void SendPhoto_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SummaryPage(patient,PhotoPath));
        }


        async void PickImage_Clicked(Object sender, EventArgs e)
        {

            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (photo != null)
            {
                await LoadPhotoAsync(photo);
                resultImage.Source = ImageSource.FromFile(PhotoPath);
            }

        }

        async void TakeImage_Clicked(Object sender, EventArgs e)
        {
            await TakePhotoAsync();
            resultImage.Source = ImageSource.FromFile(PhotoPath);
        }

        async void Analyze_Clicked(System.Object sender, System.EventArgs e)
        {
            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            AWS.IdentityPoolId,
            RegionEndpoint.USEast1
            );

            S3Uploader uploader = new S3Uploader(credentials);
            string bucketName = "babytech-images";
            string keyName = patient.PatientID + ".jpg";
            await uploader.UploadFileAsync(bucketName, keyName, PhotoPath);

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(credentials, Amazon.RegionEndpoint.USEast1);

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object
                    {
                        Bucket = bucketName,
                        Name = keyName
                    }
                },
                Attributes = new List<String>() { "ALL" }
            };

            try
            {
                DetectFacesResponse detectFacesResponse = await rekognitionClient.DetectFacesAsync(detectFacesRequest);

                foreach (FaceDetail face in detectFacesResponse.FaceDetails)
                {
                    // check if mouth is open 
                    if ((face.MouthOpen != null) && (face.MouthOpen.Value))
                    {
                        FacialAnalysisData += "\n❌ Baby's mouth should be closed";
                    }
                    if ((face.MouthOpen != null) && (!face.MouthOpen.Value) && (face.EyesOpen.Confidence > 0.88F))
                    {
                        FacialAnalysisData += "\n✔ Baby's mouth should be closed";
                    }
                   
                    // check if eyes are open

                    if ((face.EyesOpen != null) && (face.EyesOpen.Value))
                    {
                        FacialAnalysisData += "\n✔ Baby's eyes should be open";
                    }
                    if ((face.EyesOpen != null) && (!face.EyesOpen.Value) && (face.EyesOpen.Confidence > 0.93F))
                    {
                        FacialAnalysisData += "\n❌ Baby's eyes should be open";
                    }

                    // check for eyeglasses
                    if ((face.Eyeglasses != null) && (face.Eyeglasses.Value))
                    {
                        FacialAnalysisData += "\n❌ Baby should not be wearing eyeglasses";
                    }
                    if ((face.Eyeglasses != null) && (!face.Eyeglasses.Value))
                    {
                        FacialAnalysisData += "\n✔ Baby should not be wearing eyeglasses";
                    }

                    //check brightness
                    if ((face.Quality.Brightness != null) &&(face.Quality.Brightness > 0.61F) && (face.Quality.Brightness < 0.97F)) 
                    {

                        FacialAnalysisData += "\n✔  Picture is acceptable brightness";
                    }
                    else
                    {
                        FacialAnalysisData += "\n❌  Picture is acceptable brightness";
                    }

                    //check sharpness
                    if ((face.Quality.Sharpness != null) && (face.Quality.Sharpness > 0.67F)) 
                    {
                        FacialAnalysisData += "\n✔  Picture is acceptable sharpness";
                    }
                    else
                    {
                        FacialAnalysisData += "\n❌ Picture is acceptable sharpness";
                    }


                    // check for smile
                    if ((face.Smile != null) && (face.Smile.Value) && (face.Smile.Confidence < 0.83F))
                    {
                        FacialAnalysisData += "\n❌ Baby should not be smiling";
                    }
                    if ((face.Eyeglasses != null) && (!face.Eyeglasses.Value))
                    {
                        FacialAnalysisData += "\n✔ Baby should not be smiling";

                    }

                    
                    // check for calm expression
                    Emotion calmEmotion = face.Emotions.Find(emotion => emotion.Type == "CALM");

                    if (calmEmotion.Confidence > 0.93F)
                    {
                        FacialAnalysisData += "\n ✔ Baby should have a neutral facial expression";
                    }
                    else
                    {
                        FacialAnalysisData += "\n ❌ BBaby should have a neutral facial expression";
                    }

                   
                    //check sharpness
                    if ((face.Quality.Sharpness != null) && (face.Quality.Sharpness > 0.67F))
                    {
                        FacialAnalysisData += "\n✔  Picture is acceptable sharpness";
                    }
                    else
                    {
                        FacialAnalysisData += "\n❌ Picture is acceptable sharpness";
                    }

                    //check brightness
                    if ((face.Quality.Brightness != null) && (face.Quality.Brightness > 0.61F) && (face.Quality.Brightness < 0.97F))
                    {

                        FacialAnalysisData += "\n✔  Picture is acceptable brightness";
                    }
                    else
                    {
                        FacialAnalysisData += "\n❌  Picture is acceptable brightness";
                    }


                }

                await DisplayAlert("Analysis Results", FacialAnalysisData, "OK");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine("Photo Path: " + PhotoPath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }

    }
}