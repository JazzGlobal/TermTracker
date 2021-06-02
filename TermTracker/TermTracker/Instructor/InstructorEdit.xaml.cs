using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            instructorNameValue.Text = instructor.Name;
            instructorEmailValue.Text = instructor.Email;
            instructorPhoneNumberValue.Text = instructor.PhoneNumber;
        }

        private void OnSaveButtonClicked(object sender, EventArgs args)
        {
            instructor.Name = instructorNameValue.Text;
            instructor.PhoneNumber = instructorPhoneNumberValue.Text;
            instructor.Email = instructorEmailValue.Text;
            Debug.WriteLine("Save Instructor Button Clicked!");
        }
    }
}