//kriner-raz1
//This code creates the Student model for the Contoso University database. The ID is automatically set as the primary key because
//of its name and is associated with the first name of the student. The Enrollments property is used as a navigation property as it
//may contain multiple Enrollment entities. Enrollment rows are related to a student row according to the student's StudentID.
namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}