using System.Collections.Generic;

namespace TermTracker.Instructor
{
    public class Instructor
    {
        public static List<Instructor> AvailableInstructors = new List<Instructor>();

        private string name;
        private string phoneNumber;
        private string email;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public Instructor(string name, string phoneNumber, string email)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
