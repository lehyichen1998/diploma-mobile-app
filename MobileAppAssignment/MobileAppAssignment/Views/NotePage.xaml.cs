using MobileAppAssignment.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppAssignment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        private ObservableCollection<NoteModal> noteModallist=new ObservableCollection<NoteModal>();

        private Student students;
        public NotePage(Student student)
        {
            students = student;
            var result = APPDatabase_db_.readNote(student);
            noteModallist = result;
            BindingContext = noteModallist;
            InitializeComponent();
            myList.ItemsSource = noteModallist;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NoteView(students)) { Title = "Note" });
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var searchh = (sender as MenuItem).CommandParameter as NoteModal;
            APPDatabase_db_.DeleteStudent(searchh);
            noteModallist.Remove(searchh);
        }

        //private async void MyList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    NoteModal stu = e.SelectedItem as NoteModal;
        //    //var result = APPDatabase_db_.getStudentData(stu);
           
        //    //Navigation.PushModalAsync(new NoteView(stu) { Title = "Note" });
        //    await Navigation.PushModalAsync(new NavigationPage(new NoteView(stu)) { Title = "Note" });

        //}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = noteModallist.Where(s => s.Title.Contains(e.NewTextValue));
            myList.ItemsSource = new ObservableCollection<NoteModal>(result);
        }

        private async void MyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //NoteModal stu = e.SelectedItem as NoteModal;
           
            await Navigation.PushModalAsync(new NavigationPage(new NoteView(e.Item as NoteModal)) { Title = "Note" });
        }
    }
}