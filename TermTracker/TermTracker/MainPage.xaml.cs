using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.IO;
using SQLite;
using System.Collections.Generic;
using Plugin.LocalNotifications;

namespace TermTracker
{
    public partial class MainPage : ContentPage
    {

        const int MaximumTerms = 4;

        // Read this in via the database in final version.
        ObservableCollection<Term> terms = new ObservableCollection<Term>();
        public ObservableCollection<Term> Terms = new ObservableCollection<Term>();
        public static string AndroidPath = FileAccessHelper.GetLocalFilePath("test.db3");
        public MainPage()
        {
            InitializeDataFromDatabase();
            Instructor.Instructor.AvailableInstructors.Add(new Instructor.Instructor("Chris Gambrell", "317-555-1234", "cgambr2@wgu.edu"));
            Instructor.Instructor.AvailableInstructors.Add(new Instructor.Instructor("John Apple", "314-555-8956", "japple@wgu.edu"));
            Instructor.Instructor.AvailableInstructors.Add(new Instructor.Instructor("Marky Mark", "369-555-3657", "mmark@wgu.edu"));

            terms.Add(new Term("Term 1", DateTime.Now));
            terms.Add(new Term("Term 2", DateTime.Now));

            InitializeComponent();
            var conn = new SQLiteConnection(AndroidPath);
            List<Term> FinalTermList = new List<Term>();
            foreach (Term term in conn.Table<Term>().ToList())
            {
                term.Courses = term.DeserializeCourses(term.FinalSerialized);
                FinalTermList.Add(term);
            }
            TermList.ItemsSource = FinalTermList;
            Term.TermDatabaseChanged += ScheduleNotifications;
        }

        private void InitializeDataFromDatabase()
        {
            SQLiteConnection conn = new SQLiteConnection(AndroidPath);
            var result = conn.CreateTable<Instructor.Instructor>();
            
            conn.DeleteAll<Term>();
            // conn.DeleteAll<Instructor.Instructor>();

            conn.CreateTable<Term>();
            Console.WriteLine($"Database Creation Result: {result}");
            
            if (Instructor.Instructor.GetAllInstructors(conn).Count == 0)
            {
                // Generate instructors and save to the database.
                Instructor.Instructor.AddNewInstructor(conn, new Instructor.Instructor("Chris Gambrell", "317-555-1234", "cgambr2@wgu.edu"));
                Instructor.Instructor.AddNewInstructor(conn, new Instructor.Instructor("John Apple", "314-555-8956", "japple@wgu.edu"));
                Instructor.Instructor.AddNewInstructor(conn, new Instructor.Instructor("Marky Mark", "369-555-3657", "mmark@wgu.edu"));
            }
            if (Term.GetAllTerms(conn).Count == 0)
            {
                // Generate terms and save to the database.
                Term.AddNewTerm(conn, Term.GenerateDefaultTerm("Default Term 1"));
                Term.AddNewTerm(conn, Term.GenerateDefaultTerm("Default Term 2"));
            }
            Debug.WriteLine($"Instructors: {Instructor.Instructor.GetAllInstructors(conn).Count}");
            
            Term term = Term.GetAllTerms(conn)[0];
            var term_course = term.FinalSerialized;
            Debug.WriteLine($"Term Courses Serialized: {term_course}");
            Debug.WriteLine($"Term Course: {term.DeserializeCourses(term_course)[0].CourseName}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Reload();
        }
        private async void termLvItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView listView = (ListView)sender;
            Term selectedItem = (Term)listView.SelectedItem;
            string result = await DisplayActionSheet("View / Edit Term", "Cancel", null, new string[] { "View", "Edit", "Delete" });

            // Pass the selected term's data to the next form (view or edit form).
            switch (result)
            {
                case "View":
                    Debug.WriteLine($"Viewing the {selectedItem.DisplayName.ToUpper()} term!");
                    await Navigation.PushAsync(new TermView(ref selectedItem));
                    break;
                case "Edit":
                    Debug.WriteLine($"Editing the {selectedItem.DisplayName.ToUpper()} term!");
                    await Navigation.PushAsync(new TermEdit(ref selectedItem));
                    break;
                case "Delete":
                    Debug.WriteLine($"Deleting the {selectedItem.DisplayName.ToUpper()} term!");
                    SQLiteConnection conn = new SQLiteConnection(AndroidPath);
                    Term.DeleteTerm(conn, selectedItem);
                    conn.Close();
                    Reload();
                    break;
            }
        }

        private void OnClickAddTerm(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(AndroidPath);
            if(conn.Table<Term>().ToList().Count >= MaximumTerms)
            {
                DisplayAlert("Too Many Terms!", $"You can only have {MaximumTerms} terms at once.", "OK");
            } else
            {
                Term term = Term.GenerateDefaultTerm();
                Term.AddNewTerm(conn, term);
            }
            conn.Close();
            OnAppearing();
        }

        private void OnClickReload(object sender, EventArgs e)
        {
            Reload();
        }

        private void Reload()
        {
            SQLiteConnection conn = new SQLiteConnection(AndroidPath);
            List<Term> FinalTermList = new List<Term>();
            foreach (Term term in conn.Table<Term>().ToList())
            {
                term.Courses = term.DeserializeCourses(term.FinalSerialized);
                FinalTermList.Add(term);
            }
            TermList.ItemsSource = FinalTermList;
        }
        public void ScheduleNotifications(object sender, EventArgs e)
        {
            Debug.WriteLine("TIME TO SCHEDULE NOTIFICATIONS SINCE TERMS JUST GOT UPDATED!!!");
            SQLiteConnection conn = new SQLiteConnection(AndroidPath);
            List<Term> terms = conn.Table<Term>().ToList();
            int i = 0; // Notification ID Counter.
            foreach (Term term in terms)
            {
                term.Courses = term.DeserializeCourses(term.FinalSerialized);
                foreach (Course course in term.Courses)
                {
                    if(course.EnableNotifications)
                    {
                        ScheduleSingleNotification("Course Starting", $"{course.CourseName}", i, course.CourseStart);
                        i++;
                        ScheduleSingleNotification("Course Ending", $"{course.CourseName}", i, course.CourseEnd);
                        i++;
                    
                        foreach (Assessment.Assessment assessment in course.Assessments)
                        {
                            ScheduleSingleNotification("Assessment Starting", $"{course.CourseName}'s \"{assessment.AssessmentName}\" assessment is starting", i, assessment.AssessmentStart);
                            i++;
                            ScheduleSingleNotification("Assessment Ending", $"{course.CourseName}'s \"{assessment.AssessmentName}\" assessment is ending", i, assessment.AssessmentEnd);
                            i++;
                        }
                    }
                }
            }
        }
        private void ScheduleSingleNotification(string title, string body, int id, DateTime time)
        {
            if(!(DateTime.Now >= time))
            {
                // Schedule 
                Debug.WriteLine($"Scheduling notification for {title}: {body}");
                CrossLocalNotifications.Current.Show(title, body, id, time);
            }
        }
    }
}