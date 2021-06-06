
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        ObservableCollection<Assessment.Assessment> assessments;
        Course course;
        public ObservableCollection<Assessment.Assessment> Assessments = new ObservableCollection<Assessment.Assessment>();
        public CourseView(Course course)
        {
            InitializeComponent();
            this.course = course;
            assessments = new ObservableCollection<Assessment.Assessment>(course.Assessments);
            courseNameLabel.Text = course.CourseName;
            courseDurationLabel.Text = $"{course.CourseStart:MM-dd-yyyy} - {course.CourseEnd:MM-dd-yyyy}";
            courseInstructorNameValue.Text = course.Instructor.Name;
            courseInstructorEmailValue.Text = course.Instructor.Email;
            courseInstructorPhoneValue.Text = course.Instructor.PhoneNumber;
            courseStatusValue.Text = course.Status.ToString();
            courseNotesValue.Text = course.Notes;
            Reload();
            courseAssessmentsListView.ItemsSource = Assessments;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!course.DisplayNotes)
            {
                courseNotesLabel.IsVisible = false;
                courseNotesValue.IsVisible = false;
                shareButton.IsVisible = false;
            } else
            {
                courseNotesLabel.IsVisible = true;
                courseNotesValue.IsVisible = true;
                shareButton.IsVisible = true;
            }
            Reload();
        }
        private void Reload()
        {
            Assessments.Clear();
            foreach(var assessment in assessments)
            {
                Assessments.Add(assessment);
                Debug.WriteLine(assessment.AssessmentName);
            }
        }
        private async void ShareButton_Clicked(object sender, EventArgs args)
        {
            await ShareNotes();
        }
        private async Task ShareNotes()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = courseNotesValue.Text,
                Title = $"{course.CourseName} Notes"
            });
        }
    }
}