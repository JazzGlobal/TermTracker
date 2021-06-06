using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Diagnostics;

namespace TermTracker.Instructor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructorAdd : ContentPage
    {
        public InstructorAdd()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(MainPage.AndroidPath);

                if (!InstructorHelperFunctions.IsValidEmail(instructorEmailValue.Text))
                {
                    throw new InvalidEmailException("That wasn't a valid email!");
                }
                else if (string.IsNullOrEmpty(instructorNameValue.Text))
                {
                    throw new EmptyNameException("Name information cannot be blank!");
                }
                else if (string.IsNullOrEmpty(instructorPhoneNumberValue.Text))
                {
                    throw new EmptyPhoneNumberException("Phone information cannot be blank!");
                }

                Instructor instructor = new Instructor(instructorNameValue.Text, instructorPhoneNumberValue.Text, instructorEmailValue.Text);
                Instructor.AddNewInstructor(conn, instructor);
                
                
                await Navigation.PopAsync();
            } catch (Exception e)
            {
                Debug.WriteLine(e);
                await DisplayAlert("Invalid Input", e.Message, "OK");
            }

        }
    }
}