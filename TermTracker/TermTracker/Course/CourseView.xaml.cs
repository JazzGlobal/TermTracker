
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        ObservableCollection<Assessment> assessments;
        public ObservableCollection<Assessment> Assessments { get { return assessments; } }
        public CourseView(Course course)
        {
            InitializeComponent();
            courseNameLabel.Text = course.CourseName;
            courseDurationLabel.Text = $"{course.CourseStart:MM-dd-yyyy} - {course.CourseEnd:MM-dd-yyyy}";
            courseInstructorValue.Text = course.Instructor;
            courseStatusValue.Text = course.Status.ToString();
            courseNotesValue.Text = course.Notes;
            courseAssessmentsListView.ItemsSource = Assessments;
            assessments = new ObservableCollection<Assessment>(course.Assessments);

            courseAssessmentsListView.ItemsSource = Assessments;
        }

        private async void OnAssessmentItemTapped(object sender, ItemTappedEventArgs args)
        {
            Assessment selectedAssessment = (Assessment)args.Item;
            var result = await DisplayActionSheet($"View / Edit {selectedAssessment.AssessmentName}", "Cancel", null, new string[] {"View", "Edit"});
            switch (result)
            {
                case "View":
                    Debug.WriteLine("Viewing Assessment: {0}", selectedAssessment.AssessmentName);
                    break;
                case "Edit":
                    Debug.WriteLine("Editing Assessment: {0}", selectedAssessment.AssessmentName);
                    break;
                case "Cancel":
                    Debug.WriteLine("Cancelled the tap for {0}", selectedAssessment.AssessmentName);
                    break;
            }
        }
    }
}