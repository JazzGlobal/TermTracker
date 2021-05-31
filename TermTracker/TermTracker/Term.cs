using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker
{
    public class Term
    {
        public string DisplayName { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime TermEnd { get; }

        public string FormattedTermTitle { get { return $"{DisplayName}\n{TermStart.ToString("MM-dd-yyyy")} - {TermEnd.ToString("MM-dd-yyyy")}"; } }

        public Term(string displayName, DateTime termStart)
        {
            DisplayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
        }

        public Term()
        {
        }
    }
}