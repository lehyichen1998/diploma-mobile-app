using MobileAppAssignment.Models;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        private Student students;
        public ChangePassword(Student student)
        {
            InitializeComponent();
            students = student;
            newPassword.IsPassword = true;
            Showbtn.Text = "Show";
        }

        private async void submitBtn(object sender, EventArgs e)
        {
            ScryptEncoder sc = new ScryptEncoder();
            bool checkpass = sc.Compare(currentPassword.Text, students.password);
            
            if (checkpass==false)
            {
                await DisplayAlert("Password", "Invalid Password", "OK");
                return;
            }
            password.IsVisible = true;
        }
        private async void Cancelbtn(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private void Showbtn_Clicked(object sender, EventArgs e)
        {
            if (newPassword.IsPassword == true)
            {
                newPassword.IsPassword = false;
                Showbtn.Text = "Hide";

            }
            else if (newPassword.IsPassword == false)
            {
                newPassword.IsPassword = true;
                Showbtn.Text = "Show";
            }
        }
        private async void ComfirmBtn(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comfirmPassword.Text)||string.IsNullOrEmpty(newPassword.Text))
            {
                await DisplayAlert("Error", "Please Fill Up All Entry.", "OK");
                return;
            }
            if (comfirmPassword.Text != newPassword.Text)
            {
                await DisplayAlert("Error", "Please make sure the password is Correct", "OK");
                return;
            }
            string pass = Register.scryptEncoder.Encode(comfirmPassword.Text);
            students.password = pass;
            APPDatabase_db_.UpdateDetail(students);
            await DisplayAlert("Change Password","Successful","OK");
            await Navigation.PopModalAsync();
        }
    }
}