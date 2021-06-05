using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using SQLite;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace TermTracker
{
    [Table("terms")]
    public class Term : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        private string displayName;
        [MaxLength(100)]
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        private DateTime termStart;
        [MaxLength(250)]
        public DateTime TermStart
        {
            get { return termStart; }
            set
            {
                termStart = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermStart"));
            }
        }
        private DateTime termEnd;
        [MaxLength(250)]
        public DateTime TermEnd
        {
            get { return termEnd; }
            set
            {
                termEnd = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TermEnd"));
            }
        }
        private List<Course> courses;
        [Ignore]
        public List<Course> Courses
        {
            get { return courses; }
            set
            {
                courses = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Courses"));
            }
        }
        [Column("Courses")]
        public string FinalSerialized
        {
            get; 
            set;
        }
        [Ignore]
        public string SerializedCourses
        {
            get 
            {
                string jsonString = "";
                if(courses != null)
                {
                    Debug.WriteLine($"Courses Length: {courses.Count}");
                    jsonString = JsonSerializer.Serialize(courses);
                }
                FinalSerialized = jsonString;
                Debug.WriteLine(FinalSerialized);
                return jsonString;
            }
        }
        public List<Course> DeserializeCourses(string json)
        {
            List<Course> deserializedCourses = JsonSerializer.Deserialize<List<Course>>(json);
            return deserializedCourses;
        }
        public string FormattedTermTitle { get { return $"{DisplayName}\n{TermStart.ToString("MM-dd-yyyy")} - {TermEnd.ToString("MM-dd-yyyy")}"; } }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Term(string displayName, DateTime termStart)
        {
            this.displayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
            this.courses = new List<Course>();
        }

        public Term(string displayName, DateTime termStart, List<Course> courses)
        {
            DisplayName = displayName;
            TermStart = termStart;
            TermEnd = termStart.AddMonths(6);
            this.courses = courses;
            var hey = SerializedCourses;
        }
        public Term()
        {

        }

        public static List<Term> GetAllTerms(SQLiteConnection conn)
        {
            List<Term> termList = conn.Table<Term>().ToList();
            return termList;
        }
        public static int AddNewTerm(SQLiteConnection conn, Term term)
        {
            var result = conn.Insert(term);
            return result;
        }
        public static void UpdateTerm(SQLiteConnection conn, Term term)
        {
            var serialized = term.SerializedCourses;
            var rowsUpdated = conn.Update(term);
            Debug.WriteLine($"Updated {rowsUpdated} rows for Term: {term.DisplayName}");
        }
        public static void DeleteTerm(SQLiteConnection conn, Term term)
        {
            conn.Delete(term);
        }
    }
}