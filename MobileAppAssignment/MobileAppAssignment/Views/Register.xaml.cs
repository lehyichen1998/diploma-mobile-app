using MobileAppAssignment.Models;
using MobileAppAssignment.Views;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public static ScryptEncoder scryptEncoder = new ScryptEncoder();
        public Register()
        {
            InitializeComponent();
            Password.IsPassword = true;
            Showbtn.Text = "Show";
        }

        private void Showbtn_Clicked(object sender, EventArgs e)
        {
            if (Password.IsPassword == true)
            {
                Password.IsPassword = false;
                Showbtn.Text = "Hide";

            }
            else if (Password.IsPassword == false)
            {
                Password.IsPassword = true;
                Showbtn.Text = "Show";

            }
        }
        private void Cancelbtn(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private void Clearbtn(object sender, EventArgs e)
        {
            name.Text = "";
            email.Text = "";
            Password.Text = "";
            comfirmpassword.Text = "";
            phone.Text = "";
            programme.SelectedItem = false;
            Gender.SelectedItem = false;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(name.Text) || string.IsNullOrEmpty(email.Text) || string.IsNullOrEmpty(Password.Text)
                || string.IsNullOrEmpty(comfirmpassword.Text) || string.IsNullOrEmpty(phone.Text) || programme.SelectedItem==null
                || Gender.SelectedItem == null)
            {
                DisplayAlert("Error", "Please Fill Up All Entry.", "OK");
                return;
            }
            if (Password.Text != comfirmpassword.Text)
            {
                DisplayAlert("Error", "Please make sure the password is Correct", "OK");
                return;
            }
            if (APPDatabase_db_.ComfirmEmail(email.Text) == true)
            {
                DisplayAlert("Error", "Email Duplicate", "OK");
                return;
            }
            //if(Password.Text.Length<8||Password.Text.Length>15)
            //{
            //    DisplayAlert("Error","Password Must within 8 To 15 words","");
            //    return;
            //}
            var stu = new Student();
            stu.studentName = name.Text;
            stu.email = email.Text;
            string pass = scryptEncoder.Encode(comfirmpassword.Text);
            stu.password = pass;
            stu.phoneNo = phone.Text;
            stu.Programme = (programme.SelectedItem).ToString();
            stu.gender = (Gender.SelectedItem).ToString();
            stu.DateEnroll = /*DateTime.Now;*/datePicker.Date;
            stu.photo = "person.png";
            stu.ActivateCode = random(6);
            stu.isActivate = false;
            APPDatabase_db_.AddStudent(stu);
            //SendEmailWebMail(stu.ActivateCode, stu.email, stu.studentName);
            Navigation.PushModalAsync(new LoginActivate(stu));
            DisplayAlert("Add Student", stu.studentName + " Added successfull", "OK");
            DisplayAlert("Email", "Please check your Email", "OK");

        }
        public static string random(int num)
        {
            Random RandomActivateCode = new Random();
            int numberCharacter = num;
            char[] word = new char[numberCharacter];
            string accountnumber = "";
            for (int i = 0; i < numberCharacter; i++)
            {
                word[i] = Convert.ToChar(RandomActivateCode.Next(48, 57));
                accountnumber = accountnumber + word[i];
            }
            return accountnumber;
        }
    }
}