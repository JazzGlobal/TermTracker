using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TermTracker
{
    public partial class CourseEdit : ContentPage
    {
        Course course;
        public CourseEdit(ref Course course)
        {
            InitializeComponent();
            List<Instructor.Instructor> instructors = Instructor.Instructor.AvailableInstructors;
            this.course = course;
            courseNameValue.Text = course.CourseName;
            courseStartValue.Date = course.CourseStart;
            courseEndValue.Date = course.CourseEnd;
            courseNotesValue.Text = course.Notes;
            courseInstructorValue.ItemsSource = instructors;
            courseInstructorValue.ItemDisplayBinding = new Binding("Name");
            int startingIndex = instructors.IndexOf(course.Instructor);
            courseInstructorValue.SelectedIndex = startingIndex;
            courseStatusPicker.Items.Add(Course.CourseStatus.Ongoing.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Scheduled.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Withdrawn.ToString());
            courseStatusPicker.SelectedIndex = (int)course.Status;

        }
        private void OnSaveButtonClicked(object sender, EventArgs args)
        {
            course.CourseName = courseNameValue.Text;
            course.CourseStart = courseStartValue.Date;
            course.CourseEnd = courseEndValue.Date;
            course.Notes = courseNotesValue.Text;
            course.Instructor = (Instructor.Instructor) courseInstructorValue.SelectedItem;
            course.Status = (Course.CourseStatus) courseStatusPicker.SelectedIndex;
            Navigation.PopAsync();
        }

        private async void OnAddInstructorButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Instructor.InstructorAdd());
        }        
        private async void OnEditInstructorButtonClicked(object sender, EventArgs args)
        {
            var instructor = (Instructor.Instructor)courseInstructorValue.SelectedItem;
            await Navigation.PushAsync(new Instructor.InstructorEdit(ref instructor));
        }
    }
}
