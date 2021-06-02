using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Instructor
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructorAdd : ContentPage
    {
        public InstructorAdd()
        {
            InitializeComponent();
        }

        private void OnSaveButtonClicked(object sender, EventArgs args)
        {
            Instructor.AvailableInstructors.Add(new Instructor(instructorNameValue.Text,instructorPhoneNumberValue.Text,instructorEmailValue.Text));
            Navigation.PopAsync();
        }
    }
}