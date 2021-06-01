using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TermTracker
{
    public class Term : INotifyPropertyChanged
    {
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        private DateTime termStart;
        public DateTime TermStart
        {
            get { return termStart; }
            set
            {
                termStart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermStart"));
            }
        }
        private DateTime termEnd;
        public DateTime TermEnd
        {
            get { return termEnd; }
            set
            {
                termEnd = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermEnd"));
            }
        }
        private List<Course> courses;
        public List<Course> Courses
        {
            get { return courses; }
            set
            {
                courses = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Courses"));
            }
        }
        public string FormattedTermTitle { get { return $"{DisplayName}\n{TermStart.ToString("MM-dd-yyyy")} - {TermEnd.ToString("MM-dd-yyyy")}"; } }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Term(string displayName, DateTime termStart)
        {
            this.displayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
            Courses = new List<Course>();
        }

        public Term(string displayName, DateTime termStart, List<Course> courses)
        {
            DisplayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
            Courses = courses;
        }
        public Term()
        {

        }

    }
}