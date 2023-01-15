//kriner-raz1
//This code creates the Enrollment model for the database and is associated with a Student entity and a Course entity. 
//The EnrollmentID property is the primary key and the StudentID and CourseID properties are the foreign keys and they
//are related to their corresponding navigation properties, namely Student and Course.
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}