using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Instructor
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {

        }
    }
    public class EmptyNameException : Exception
    {
        public EmptyNameException(string message) : base(message)
        {

        }
    }
    public class EmptyPhoneNumberException : Exception
    {
        public EmptyPhoneNumberException(string message) : base(message)
        {

        }
    }
}
