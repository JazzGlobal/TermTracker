using System;

namespace TermTracker
{
    public class Course
    {
        public string CourseName { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public string FormattedCourseTitle { get { return $"{CourseName}\n{CourseStart.ToString("MM-dd-yyyy")} - {CourseEnd.ToString("MM-dd-yyyy")}"; } }

        public Course(string courseName, DateTime startDate, DateTime endDate)
        {
            CourseName = courseName;
            CourseStart = startDate;
            CourseEnd = endDate;
        }
    }
}
