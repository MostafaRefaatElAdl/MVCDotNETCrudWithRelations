using Day03App.Models;

namespace Day04App.Models
{
    public class DepartmentCourseViewModel
    {
        public List<Course> CoursesInDept { get; set; }
        public List<Course> CoursesNotInDept { get; set; }
        public Department Department { get; set; }
    }
}
