
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            courseInstructorValue.Text = course.Instructor;
            courseStatusValue.Text = course.Status.ToString();
            courseNotesValue.Text = course.Notes;
            Reload();
            courseAssessmentsListView.ItemsSource = Assessments;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Reload();
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
                    Assessments.Remove(selectedAssessment);
                    course.Assessments = new List<Assessment.Assessment>(Assessments);
                    break;
                case "Cancel":
                    Debug.WriteLine($"Cancelled the tap for {selectedAssessment.AssessmentName}");
                    break;
            }
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
    }
}