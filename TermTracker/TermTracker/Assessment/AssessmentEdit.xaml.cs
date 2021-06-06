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
    public partial class AssessmentEdit : ContentPage
    {
        Assessment assessment;
        public AssessmentEdit(ref Assessment assessment)
        {
            this.assessment = assessment;
            InitializeComponent();
            assessmentNameTitle.Text = assessment.AssessmentName;
            assessmentNameLabel.Text = assessment.AssessmentName;
            assessmentNameValue.Text = assessment.AssessmentName;
            assessmentStartDateValue.Date = assessment.AssessmentStart;
            assessmentEndDateValue.Date = assessment.AssessmentEnd;

            assessmentTypeValuePicker.Items.Add(Assessment.AssessmentType.Objective.ToString());
            assessmentTypeValuePicker.Items.Add(Assessment.AssessmentType.Performance.ToString());
            assessmentTypeValuePicker.SelectedIndex = (int)assessment.Type;
        }

        private void OnSaveButtonClicked(object sender, EventArgs args)
        {
            assessment.AssessmentName = assessmentNameValue.Text;
            assessment.AssessmentStart = assessmentStartDateValue.Date;
            assessment.AssessmentEnd = assessmentEndDateValue.Date;
            assessment.Type = (Assessment.AssessmentType) assessmentTypeValuePicker.SelectedIndex;
            Debug.WriteLine("Save Assessment Details Button Clicked!");
            Navigation.PopAsync();
        }
    }
}