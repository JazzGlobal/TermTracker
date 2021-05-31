using System;

namespace TermTracker
{
    public class Course
    {
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Course(string courseName, DateTime startDate, DateTime endDate)
        {
            CourseName = courseName;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
