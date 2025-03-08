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
    public partial class NoteView : ContentPage
    {
        private NoteModal noteModal=new NoteModal();
        public NoteView(Student student)
        {
            noteModal.StudentId = student.studentId;
            InitializeComponent();
            BindingContext = noteModal;
        }
        public NoteView(NoteModal noteModals)
        {
            noteModal = noteModals;
            InitializeComponent();
            BindingContext = noteModal;
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Title","Empty","OK");
                return;
            }
            if (noteModal.NoteModalId != 0)
            {
                noteModal.Title = TitleEntry.Text;
                noteModal.Content = ContentEntry.Text;
                var updateNote=APPDatabase_db_.UpdateNote(noteModal);
                await DisplayAlert("Update","successful","OK");
                App.Current.MainPage = new NavigationPage(new Home(updateNote));
                return;
            }
            var result = new NoteModal();
            result = noteModal;
            result.Title = TitleEntry.Text;
            result.Content = ContentEntry.Text;
            var res=APPDatabase_db_.SaveNote(result);
            await DisplayAlert("Save Note","Successful","OK");
            App.Current.MainPage = new NavigationPage(new Home(res));

        }
    }

}