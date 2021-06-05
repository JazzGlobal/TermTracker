using System;

namespace TermTracker.Assessment
{
    public class Assessment
    {
        public string AssessmentName { get; set; }
        public DateTime AssessmentStart { get; set; }
        public DateTime AssessmentEnd { get; set; }
        public AssessmentType Type { get; set; }
        public enum AssessmentType
        {
            Objective = 0,
            Performance = 1
        }

        public Assessment(string AssessmentName, DateTime AssessmentStart, DateTime AssessmentEnd, AssessmentType Type)
        {
            this.AssessmentName = AssessmentName;
            this.AssessmentStart = AssessmentStart;
            this.AssessmentEnd = AssessmentEnd;
            this.Type = Type;
        }
    }
}
