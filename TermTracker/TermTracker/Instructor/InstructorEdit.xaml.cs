using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Instructor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructorEdit : ContentPage
    {
        Instructor instructor;
        public InstructorEdit(ref Instructor instructor)
        {
            this.instructor = instructor;
            InitializeComponent();
            instructorEditLabel.Text = instructor.Name;
            instructorNameValue.Text = instructor.Name;
            instructorEmailValue.Text = instructor.Email;
            instructorPhoneNumberValue.Text = instructor.PhoneNumber;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs args)
        {
            string tempInstructorName = instructor.Name;
            string tempInstructorPhoneNumber = instructor.PhoneNumber;
            string tempInstructorEmail = instructor.Email;

            try
            {
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

                SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(MainPage.AndroidPath);
                instructor.Name = instructorNameValue.Text;
                instructor.PhoneNumber = instructorPhoneNumberValue.Text;
                instructor.Email = instructorEmailValue.Text;
                Instructor.UpdateInstructor(conn , instructor);
                conn.Close();
                await Navigation.PopAsync();
            } catch (Exception e)
            {
                await DisplayAlert("Invalid Input", e.Message, "OK");
            }
        }
    }
}