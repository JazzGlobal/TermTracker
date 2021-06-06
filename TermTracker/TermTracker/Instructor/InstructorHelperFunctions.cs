using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TermTracker.Instructor
{
    public class InstructorHelperFunctions
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress ma = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException formatEx)
            {
                return false;
            }
        }
    }
}
