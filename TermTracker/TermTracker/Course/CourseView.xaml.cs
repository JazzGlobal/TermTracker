
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        public CourseView(Course course)
        {
            InitializeComponent();
            courseNameLabel.Text = course.CourseName;
            courseDurationLabel.Text = $"{course.CourseStart:MM-dd-yyyy} - {course.CourseEnd:MM-dd-yyyy}";
            courseInstructorValue.Text = course.Instructor;
            courseStatusValue.Text = course.Status.ToString();
            courseNotesValue.Text = course.Notes;
        }
    }
}