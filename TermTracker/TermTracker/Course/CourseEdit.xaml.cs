using System;

using Xamarin.Forms;

namespace TermTracker
{
    public partial class CourseEdit : ContentPage
    {
        Course course;
        public CourseEdit(Course course)
        {
            InitializeComponent();

            this.course = course;
            courseNameLabel.Text = course.CourseName;
            courseStartValue.Date = course.CourseStart;
            courseEndValue.Date = course.CourseEnd;
            courseNotesValue.Text = course.Notes;
            courseInstructorValue.Text = course.Instructor;
            courseStatusPicker.Items.Add(Course.CourseStatus.Ongoing.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Scheduled.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Withdrawn.ToString());
            courseStatusPicker.SelectedIndex = (int) course.Status;
            
        }
        private void OnGoToAssessmentsButtonClicked(object sender, EventArgs args)
        {

        }

        private void OnSaveButtonClicked(object sender, EventArgs args)
        {

        }
    }
}
