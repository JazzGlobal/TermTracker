
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System;

namespace TermTracker
{
    public partial class TermView : ContentPage
    {
        ObservableCollection<Course> courses = new ObservableCollection<Course>();

        public ObservableCollection<Course> Courses { get { return courses; } }

        public TermView(ref Term term)
        {
            InitializeComponent();
            
            if(term.Courses.Count == 0)
            {
                PopulateTermWithCourses(ref term);
                
            }

            coursesListLabel.Text = $"Courses ({term.DisplayName})";
            CourseList.ItemsSource = term.Courses;
        }
        
        private void courseLvItemTapped (object sender, ItemTappedEventArgs e)
        {

        }
        private void OnClickAddCourse(object sender, EventArgs e)
        {

        }
        private void OnClickReload(object sender, EventArgs e)
        {

        }
        private void PopulateTermWithCourses(ref Term term)
        {
            for (int i = 0; i < 5; i++)
            {
                term.Courses.Add(new Course("Course", DateTime.Now, DateTime.Now.AddDays(90)));
            }
        }
    }
}