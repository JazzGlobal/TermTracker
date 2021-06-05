
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using SQLite;

namespace TermTracker
{
    public partial class TermView : ContentPage
    {
        ObservableCollection<Course> courses = new ObservableCollection<Course>();

        public ObservableCollection<Course> Courses = new ObservableCollection<Course>();
        Term term;
        public TermView(ref Term term)
        {
            InitializeComponent();
            this.term = term;
            if (term.Courses.Count == 0)
            {
                PopulateTermWithCourses();
            }
            // courses = new ObservableCollection<Course>(term.Courses);
            Reload();
            coursesListLabel.Text = $"Courses ({term.DisplayName})";
            CourseList.ItemsSource = term.Courses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Reload();
        }

        private async void courseLvItemTapped(object sender, ItemTappedEventArgs e)
        {
            Course selectedCourse = (Course)e.Item;
            string result = await DisplayActionSheet($"View / Edit {selectedCourse.CourseName}", "Cancel", null, new string[] { "View", "Edit", "Delete" });
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing the {selectedCourse.CourseName} course!");
                    await Navigation.PushAsync(new CourseView(selectedCourse));
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing the {selectedCourse.CourseName} course!");
                    await Navigation.PushAsync(new CourseEdit(ref selectedCourse));
                    break;
                case "Delete":
                    Debug.WriteLine($"Deleting the {selectedCourse.CourseName} course!");
                    term.Courses.Remove(selectedCourse);
                    SQLiteConnection conn = new SQLiteConnection(MainPage.AndroidPath);
                    Term.UpdateTerm(conn, term);
                    Debug.WriteLine(term.Courses.Count);
                    conn.Close();
                    Reload();
                    break;
                case "Cancel":
                    Debug.WriteLine("Cancelling with no changes!");
                    break;
            }
        }
        private void OnClickAddCourse(object sender, EventArgs e)
        {
            Debug.WriteLine("Add Course Button Pressed");
        }
        private void OnClickReload(object sender, EventArgs e)
        {
            Debug.WriteLine("Reload Button Pressed");
            Reload();
        }
        private void PopulateTermWithCourses()
        {
            Debug.WriteLine("Populating Term With COURSES!");
            List<Assessment.Assessment> assessments = new List<Assessment.Assessment>();
            assessments.Add(new Assessment.Assessment("Assessment 1", DateTime.Now, DateTime.Now.AddDays(2), Assessment.Assessment.AssessmentType.Objective));
            assessments.Add(new Assessment.Assessment("Assessment 2", DateTime.Now, DateTime.Now.AddDays(2), Assessment.Assessment.AssessmentType.Performance));

            List<Course> courses_temp = new List<Course>();
            courses_temp.Add(new Course("BIO 101", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));
            courses_temp.Add(new Course("MATH 101", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));
            courses_temp.Add(new Course("SDEV 101", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));
            courses_temp.Add(new Course("ANTH 101", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));
            courses_temp.Add(new Course("PHIL 101", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));
            courses_temp.Add(new Course("PHIL 102", DateTime.Now, DateTime.Now.AddDays(90), Course.CourseStatus.Scheduled, Instructor.Instructor.AvailableInstructors[0], "This class is easy!", assessments));

            term.Courses = courses_temp;
        }

        private void Reload()
        {
            List<Course> finalCourseList = new List<Course>();
            foreach (Course course in term.Courses)
            {
                finalCourseList.Add(course);
                Debug.WriteLine(course.CourseName);
            }
            Debug.WriteLine(finalCourseList.Count);
            CourseList.ItemsSource = finalCourseList;

            SQLiteConnection conn = new SQLiteConnection(MainPage.AndroidPath);
            Term.UpdateTerm(conn, term);
            conn.Close();
        }
    }
}