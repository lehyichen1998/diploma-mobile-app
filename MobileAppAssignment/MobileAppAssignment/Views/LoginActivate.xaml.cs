using MobileAppAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginActivate : ContentPage
    {
        private Student student;
        public LoginActivate(Student stu)
        {
            student = stu;
            student.ActivateCode = random(6);
            APPDatabase_db_.UpdateDetail(student);
            SendEmailWebMail(student.ActivateCode, student.email, student.studentName);
            InitializeComponent();
           
        }

        private  void BtnActivate_Clicked(object sender, EventArgs e)
        {
            var result = APPDatabase_db_.GetCode(ActivationCode.Text,student);
            if (result==null)
            {
                  DisplayAlert("Activation", "Invalid Activation code", "OK");
                return;
            }
            //update isActivated = true
            student.isActivate = true;
            APPDatabase_db_.UpdateDetail(student);
             DisplayAlert("Activate Account Successful", "Go To Login Page To Login", "OK");
             Navigation.PopModalAsync();
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
                mail.Body = "Dear " + name + ",\n\nYour Verification is " + bodyMessage + ".\n\nThank You.";

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