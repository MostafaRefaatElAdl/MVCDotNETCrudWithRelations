using System.ComponentModel.DataAnnotations.Schema;

namespace Day04App.Models
{
    public class StudentCourses
    {
        [ForeignKey("Student")]
        public int StdId { get; set; }
        [ForeignKey("Course")]
        public int CrsId { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
        public int? Degree { get; set; }
    }
}
