
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;
using System.Diagnostics;

namespace TermTracker
{
    public partial class TermView : ContentPage
    {
        ObservableCollection<Course> courses = new ObservableCollection<Course>();

        public ObservableCollection<Course> Courses { get { return courses; } }
        Term term;
        public TermView(Term term)
        {
            InitializeComponent();
            this.term = term;
            PopulateTermWithCourses();
            coursesListLabel.Text = $"Courses ({term.DisplayName})";
            CourseList.ItemsSource = term.Courses;
        }

        private async void courseLvItemTapped (object sender, ItemTappedEventArgs e)
        {
            Course selectedCourse = (Course)e.Item;
            string result = await DisplayActionSheet($"View / Edit {selectedCourse.CourseName}", "Cancel", null, new string[] { "View", "Edit" });
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing the {selectedCourse.CourseName} course!");
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing the {selectedCourse.CourseName} course!");
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
        }
        private void PopulateTermWithCourses()
        {
            for (int i = 0; i < 5; i++)
            {
                term.Courses.Add(new Course("Course", DateTime.Now, DateTime.Now.AddDays(90)));
            }
        }
    }
}