using MobileAppAssignment.Models;
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
    public partial class forgotPassword : ContentPage
    {
        private static Student student;
        public forgotPassword()
        {
            InitializeComponent();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Email.Text))
            {
                await DisplayAlert("Email", "Enter Email Address", "OK");
                return;
            }
            student = APPDatabase_db_.CheckEmail(Email.Text);

            if (student == null)
            {
                await DisplayAlert("Email", "Invalid Email", "OK");
                return;
            }
            student.ActivateCode = Register.random(6);
            APPDatabase_db_.UpdateDetail(student);
            SendEmailWebMail(student.ActivateCode, student.email, student.studentName);
            Comfirmcode.IsVisible = true;
        }

        private void btnComfirmCode_Clicked(object sender, EventArgs e)
        {
            if (code.Text != student.ActivateCode)
            {
                DisplayAlert("Code", "Invalid Code", "OK");
                return;
            }
            ComfirmPassword.IsVisible = true;
        }

        private async void btnComfirmPassword_Clicked(object sender, EventArgs e)
        {
            if (newComfirmPassword.Text != newPassword.Text)
            {
                await DisplayAlert("Password", "Please Comfirm The Password", "OK");
                return;
            }
            string pass = Register.scryptEncoder.Encode(newComfirmPassword.Text);
            
            student.password = pass;
            APPDatabase_db_.UpdateDetail(student);
            await DisplayAlert("Change Password","Successful","OK");
            await Navigation.PopModalAsync();
        }
        public void SendEmailWebMail(string bodyMessage, string email, string name)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("ycleh2892@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Verification Code";
                mail.Body = "Dear" + name + ",\n\nYour Verification is " + bodyMessage + ".\n\nThank You.";

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ycleh2892@gmail.com", "ycleh2892ycleh");

                SmtpServer.Send(mail);
                DisplayAlert("Email", "Please check your Email", "OK");
            }
            catch (Exception)
            {
                DisplayAlert("Email", "Problem while sending email, Please check details.", "OK");
            }
        }
    }
}