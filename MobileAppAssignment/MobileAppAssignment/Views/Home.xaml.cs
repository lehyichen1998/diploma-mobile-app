using MobileAppAssignment.Models;
using MobileAppAssignment.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        private Student students;
        public Home(Student student)
        {

            students = student;
            BindingContext = students;
            InitializeComponent();
            dateEnrolled.Text = student.DateEnroll.ToShortDateString();
            image.Source = ImageSource.FromFile(students.photo);
            Children.Add(new NotePage(students) { Title = "Note", IconImageSource = "profile.png" });
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Edit(students));
        }

        private async void Logout(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Logout", "Are You Want To Logout?", "Yes", "No");
            if (response == true)
            {
                //await Navigation.PopAsync();
                App.Current.MainPage = new MainPage();
            }
        }
        private async void changePassword(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangePassword(students));
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit", "Yes", "No");
                if (result == true)
                {
                    System.Environment.Exit(0);
                }
            });
            return true;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayActionSheet("Profile Photo", "Remove Photo", "Cancel", "Open Camera", "Browse Photo");
            if (response == "Open Camera")
            {
                TakePhoto_OnClicked(sender, e);
            }
            else if (response == "Browse Photo")
            {
                PickPhoto_OnClicked(sender, e);
            }
            else if(response=="Remove Photo")
            {
                students.photo = "person.png";
                APPDatabase_db_.UpdateDetail(students);
                image.Source = ImageSource.FromFile(students.photo);

                //removePicture(sender, e);
            }
        }
        private async void TakePhoto_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front

            });

            if (file == null)
                return;
            students.photo = file.Path;
            APPDatabase_db_.UpdateDetail(students);
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void PickPhoto_OnClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });


            if (file == null)
                return;
            students.photo = file.Path;
            APPDatabase_db_.UpdateDetail(students);
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        private async void removePicture(object sender, EventArgs e)
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });


            if (file == null)
                return;
            students.photo = "person.png";
            APPDatabase_db_.UpdateDetail(students);
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}