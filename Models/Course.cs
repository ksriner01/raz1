//kriner-raz1
//This code creates the Course model for the Contoso University database. CourseID is the primary key with a related
//title of the course and number of credits it offers. The DatabaseGenerated attribute added above the variables allows
//the application to specify the primary key rather than the database generating one on its own. Finally, a collection
//of Enrollments is once again used as multiple enrollments may apply to a single Course.
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}