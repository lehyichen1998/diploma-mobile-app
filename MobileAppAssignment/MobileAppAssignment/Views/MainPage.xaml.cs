using MobileAppAssignment.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAppAssignment
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //APPDatabase_db_.RemoveTable();
            APPDatabase_db_.CreateTable();

        }
        private async void Loginbtn_Clicked(object sender, EventArgs e)
        {
           

            var user = APPDatabase_db_.LoginChecking(email.Text, password.Text);
            if (user==null)
            {
                await DisplayAlert("Error", "Invalid Email and Password", "OK");
                return;
            }
            if (user.isActivate == false)
            {
                await DisplayAlert("Email", "Please check your Email", "OK");
                await Navigation.PushModalAsync(new LoginActivate(user));
                return;
            }
            App.Current.MainPage = new NavigationPage( new Home(user) { Title="My Profile"});

            //await Navigation.PushAsync(new Home(user));
        }
        private async void Registerbtn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Register());
        }
        private async void forgotPasswordBtn(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new forgotPassword());
        }
    }
}