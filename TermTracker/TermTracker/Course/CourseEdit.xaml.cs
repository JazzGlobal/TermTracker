using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;
using System.Diagnostics;
using System.Linq;

namespace TermTracker
{
    public partial class CourseEdit : ContentPage
    {
        Course course;
        SQLiteConnection conn;
        public CourseEdit(ref Course course)
        {
            InitializeComponent();
            conn = new SQLiteConnection(MainPage.AndroidPath);
            List<Instructor.Instructor> instructors = Instructor.Instructor.GetAllInstructors(conn);
            this.course = course;
            courseNameValue.Text = course.CourseName;
            courseStartValue.Date = course.CourseStart;
            courseEndValue.Date = course.CourseEnd;
            courseNotesValue.Text = course.Notes;
            courseInstructorValue.ItemsSource = instructors;
            var tempInstructor = course.Instructor;
            var found = from instructor in instructors.AsEnumerable()
                                where instructor.Id == tempInstructor.Id
                                select instructor;
            int startingIndex = instructors.IndexOf(found.First());
            courseInstructorValue.ItemDisplayBinding = new Binding("Name");
            courseInstructorValue.SelectedIndex = startingIndex;
            courseStatusPicker.Items.Add(Course.CourseStatus.Ongoing.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Scheduled.ToString());
            courseStatusPicker.Items.Add(Course.CourseStatus.Withdrawn.ToString());
            courseStatusPicker.SelectedIndex = (int)course.Status;
            // courseAssessmentsListView.ItemsSource = course.Assessments;
            courseEnableNotifications.IsChecked = course.EnableNotifications;
            courseDisplayNotesValue.IsChecked = course.DisplayNotes;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReloadAssessments();
            ReloadInstructors();
        }
        private void OnSaveButtonClicked(object sender, EventArgs args)
        {
            course.CourseName = courseNameValue.Text;
            course.CourseStart = courseStartValue.Date;
            course.CourseEnd = courseEndValue.Date;
            course.Notes = courseNotesValue.Text;
            course.Instructor = (Instructor.Instructor) courseInstructorValue.SelectedItem;
            course.Status = (Course.CourseStatus) courseStatusPicker.SelectedIndex;
            course.DisplayNotes = courseDisplayNotesValue.IsChecked;
            course.EnableNotifications = courseEnableNotifications.IsChecked;
            Navigation.PopAsync();
        }
        private async void OnAssessmentItemTapped(object sender, ItemTappedEventArgs args)
        {
            Assessment.Assessment selectedAssessment = (Assessment.Assessment)args.Item;
            var result = await DisplayActionSheet($"View / Edit {selectedAssessment.AssessmentName}", "Cancel", null, new string[] { "View", "Edit", "Delete" });
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing Assessment: {selectedAssessment.AssessmentName}");
                    await Navigation.PushAsync(new Assessment.AssessmentView(selectedAssessment));
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing Assessment: {selectedAssessment.AssessmentName}");
                    await Navigation.PushAsync(new Assessment.AssessmentEdit(ref selectedAssessment));
                    break;
                case "Delete":
                    Debug.WriteLine($"Deleting Assessment: {selectedAssessment.AssessmentName}");
                    course.Assessments.Remove(selectedAssessment);
                    break;
                case "Cancel":
                    Debug.WriteLine($"Cancelled the tap for {selectedAssessment.AssessmentName}");
                    break;
            }
        }
        private async void OnViewAssessmentsClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Assessment.AssessmentListView(ref course));
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
        private void ReloadInstructors()
        {
            List<Instructor.Instructor> instructors = Instructor.Instructor.GetAllInstructors(conn);
            courseInstructorValue.ItemsSource = instructors;
            var tempInstructor = course.Instructor;
            var found = from instructor in instructors.AsEnumerable()
                        where instructor.Id == tempInstructor.Id
                        select instructor;
            int startingIndex = 0;
            try
            {
                startingIndex = instructors.IndexOf(found.First());
            } catch (Exception e)
            {
                startingIndex = 0;
            }
            courseInstructorValue.ItemDisplayBinding = new Binding("Name");
            courseInstructorValue.SelectedIndex = startingIndex;
        }
        private void ReloadAssessments()
        {
            //List<Assessment.Assessment> assessments = new List<Assessment.Assessment>();
            //foreach (var assessment in course.Assessments)
            //{
            //    assessments.Add(assessment);
            //    Debug.WriteLine(assessment.AssessmentName);
            //}
            //course.Assessments = assessments;
            //courseAssessmentsListView.ItemsSource = course.Assessments;
        }
    }
}
