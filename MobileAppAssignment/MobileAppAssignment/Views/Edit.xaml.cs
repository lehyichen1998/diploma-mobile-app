using MobileAppAssignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        //private Student students;
        private Student students;

        public Edit(Student student)
        {
            InitializeComponent();
            students = student;
            BindingContext = students;
        }

        private async void updatebtn(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(name.Text)||string.IsNullOrWhiteSpace(phone.Text))
            {
                await DisplayAlert("Empty", "Please makesure Entry not Empty", "OK");
            }
            var stu = students;
            stu.studentName = name.Text;
            stu.gender = gender.SelectedItem.ToString();
            stu.phoneNo = phone.Text;
            stu.Programme = programme.SelectedItem.ToString();
            var result = APPDatabase_db_.UpdateDetail(stu);
            await DisplayAlert("Edit Detail", "Successful", "OK");
            if (result != null)
            {

                Application.Current.MainPage = new NavigationPage(new Home(BindingContext as Student))
                {
                   
                };
                return;
            }
        }

        private async void cancelbtn(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}