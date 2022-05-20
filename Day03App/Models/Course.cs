
using Day03App.Models;
using System.ComponentModel.DataAnnotations;

namespace Day04App.Models
{
    public class Course
    {
        [Required]
        public int CrsID { get; set; }
        [Required]
        public string CrsName { get; set; }
        public Course()
        {
            DepartmentCourses = new List<Department>();
            StudentCourses = new List<StudentCourses>();
        }
        public virtual List<Department> DepartmentCourses { get; set; }
        public virtual List<StudentCourses> StudentCourses { get; set; }
    }
}
