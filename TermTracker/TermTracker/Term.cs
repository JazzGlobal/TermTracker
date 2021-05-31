using System;
using System.Collections.Generic;

namespace TermTracker
{
    public class Term
    {
        public string DisplayName { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; }
        public List<Course> Courses { get; set; }
        public string FormattedTermTitle { get { return $"{DisplayName}\n{TermStart.ToString("MM-dd-yyyy")} - {TermEnd.ToString("MM-dd-yyyy")}"; } }

        public Term(string displayName, DateTime termStart)
        {
            DisplayName = displayName;
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