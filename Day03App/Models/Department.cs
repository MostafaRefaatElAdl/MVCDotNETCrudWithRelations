using Day04App.Models;
using System.ComponentModel.DataAnnotations;

namespace Day03App.Models
{
    public class Department
    {
        [Required]
        public int Id { get; set; }
        [Required,StringLength(15,MinimumLength =2)]
        public string Name { get; set; }
        [Required, StringLength(10, MinimumLength = 4)]
        public string Location { get; set; }
        // Relations >> One department to Many students
        public Department()
        {
            Students = new HashSet<Student>();
            CoursesInDept = new List<Course>();
        }
        public virtual ICollection<Student> Students { get; set; }
        public virtual List<Course> CoursesInDept { get; set; }
    }
}
