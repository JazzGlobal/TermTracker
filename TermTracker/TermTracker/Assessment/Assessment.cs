using System;

namespace TermTracker
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

        public Assessment(string name, DateTime start, DateTime end, AssessmentType type)
        {
            AssessmentName = name;
            AssessmentStart = start;
            AssessmentEnd = end;
            Type = type;
        }

    }
}
