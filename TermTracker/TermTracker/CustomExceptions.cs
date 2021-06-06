using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker
{
    public class DateSequenceInvalidException : Exception
    {
        public DateSequenceInvalidException(string message) : base(message)
        {

        }
    }
}
