
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker.Assessment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentView : ContentPage
    {
        public AssessmentView(Assessment assessment)
        {
            InitializeComponent();
            assessmentNameLabel.Text = assessment.AssessmentName;
            assessmentDurationLabel.Text = $"{assessment.AssessmentStart.ToString("MM-dd-yyyy")} - {assessment.AssessmentEnd.ToString("MM-dd-yyyy")}";
            assessmentTypeValue.Text = assessment.Type.ToString();
        }
    }
}