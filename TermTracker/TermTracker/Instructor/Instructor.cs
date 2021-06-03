using System.Collections.Generic;
using SQLite;
namespace TermTracker.Instructor
{
    [Table("instructors")]
    public class Instructor
    {
        public static List<Instructor> AvailableInstructors = new List<Instructor>();

        private string name;
        private string phoneNumber;
        private string email;
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [MaxLength(250)]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        [MaxLength(250)]
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
        public Instructor()
        {

        }
        public static List<Instructor> GetAllInstructors(SQLite.SQLiteConnection conn)
        {
            List<Instructor> gen_instructors = conn.Table<Instructor>().ToList();
            return gen_instructors;
        }
        public static int AddNewInstructor(SQLite.SQLiteConnection conn, Instructor instructor)
        {
            int result = conn.Insert(instructor);
            return result;
        }
    }
}
