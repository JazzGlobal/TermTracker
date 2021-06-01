using System;
using System.Collections.Generic;

namespace TermTracker
{
    public class Course
    {
        public string CourseName { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public CourseStatus Status { get; set; }
        public string Instructor { get; set; }
        public string Notes { get; set; }
        public List<Assessment.Assessment> Assessments { get; set; }
        public enum CourseStatus
        {
            Scheduled = 0,
            Ongoing = 1,
            Withdrawn = 2
        }
        public string FormattedCourseTitle { get { return $"{CourseName}\n{CourseStart.ToString("MM-dd-yyyy")} - {CourseEnd.ToString("MM-dd-yyyy")}"; } }

        public Course(string courseName, DateTime startDate, DateTime endDate, CourseStatus status, string instructor, string notes, List<Assessment.Assessment> assessments)
        {
            CourseName = courseName;
            CourseStart = startDate;
            CourseEnd = endDate;
            Status = status;
            Instructor = instructor;
            Notes = notes;
            Assessments = assessments;
        }
    }
}
