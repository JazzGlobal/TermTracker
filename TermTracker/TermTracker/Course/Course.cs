using System;
using System.Collections.Generic;

namespace TermTracker
{
    [Serializable]
    public class Course : ICloneable
    {
        public string CourseName { get; set; }
        public DateTime CourseStart { get; set; }
        public DateTime CourseEnd { get; set; }
        public CourseStatus Status { get; set; }
        public Instructor.Instructor Instructor { get; set; }
        public string Notes { get; set; }
        public List<Assessment.Assessment> Assessments { get; set; }
        
        public Boolean DisplayNotes { get; set; }
        public Boolean EnableNotifications { get; set; }
        public enum CourseStatus
        {
            Scheduled = 0,
            Ongoing = 1,
            Completed = 2,
            Withdrawn = 3
        }
        public string FormattedCourseTitle { get { return $"{CourseName}\n{CourseStart.ToString("MM-dd-yyyy")} - {CourseEnd.ToString("MM-dd-yyyy")}"; } }

        public Course(string CourseName, DateTime CourseStart, DateTime CourseEnd, CourseStatus Status, Instructor.Instructor Instructor, string Notes, List<Assessment.Assessment> Assessments)
        {
            this.CourseName = CourseName;
            this.CourseStart = CourseStart;
            this.CourseEnd = CourseEnd;
            this.Status = Status;
            this.Instructor = Instructor;
            this.Notes = Notes;
            this.Assessments = Assessments;
            DisplayNotes = false;
            EnableNotifications = false;
        }

        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}
