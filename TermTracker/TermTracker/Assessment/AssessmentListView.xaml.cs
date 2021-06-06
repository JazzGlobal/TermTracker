using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Assessment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentListView : ContentPage
    {
        Course course;
        public AssessmentListView(ref Course course)
        {
            InitializeComponent();
            this.course = course;
            Debug.WriteLine(course.Assessments.Count);
            assessmentListLabel.Text = $"Assessments ({course.CourseName})";
            courseAssessmentsListView.ItemsSource = course.Assessments;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ReloadAssessments();
        }
        private async void OnAssessmentItemTapped(object sender, ItemTappedEventArgs args)
        {
           Assessment selectedAssessment = (Assessment)args.Item;
            var result = await DisplayActionSheet($"View / Edit {selectedAssessment.AssessmentName}", "Cancel", null, new string[] { "View", "Edit", "Delete" });
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing Assessment: {selectedAssessment.AssessmentName}");
                    await Navigation.PushAsync(new AssessmentView(selectedAssessment));
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing Assessment: {selectedAssessment.AssessmentName}");
                    await Navigation.PushAsync(new AssessmentEdit(ref selectedAssessment));
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

        private void OnAddAssessmentClicked(object sender, EventArgs args)
        {
            course.Assessments.Add(new Assessment("Default Assessment", DateTime.Now.AddDays(7), DateTime.Now.AddDays(14), Assessment.AssessmentType.Objective));
            ReloadAssessments();
        }
        private void ReloadAssessments()
        {
            List<Assessment> assessments = new List<Assessment>();
            foreach (var assessment in course.Assessments)
            {
                assessments.Add(assessment);
                Debug.WriteLine(assessment.AssessmentName);
            }
            course.Assessments = assessments;
            courseAssessmentsListView.ItemsSource = course.Assessments;
        }
    }
}