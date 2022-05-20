using Day04App.Data;

namespace Day04App.Models
{
    public interface ICourse
    {
        public List<Course> GetAllCourses();
        public Course GetCourseById(int id);
        public Course AddCourse(Course crs);
        public void DeleteCourse(int id);
        public void UpdateCourse(Course crs);

    }
    public class CourseMoc
    {


    }
    public class CourseDB : ICourse
    {
        DatabaseITI db;
        public CourseDB(DatabaseITI db)
        {
            this.db = db;
        }
        public Course AddCourse(Course crs)
        {
            db.Courses.Add(crs);
            db.SaveChanges();
            return crs;
        }

        public void DeleteCourse(int id)
        {
            db.Courses.Remove(GetCourseById(id));
            db.SaveChanges();
        }

        public List<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return db.Courses.FirstOrDefault(x => x.CrsID == id);
        }

        public void UpdateCourse(Course crs)
        {
            Course oldcrs = db.Courses.FirstOrDefault(x => x.CrsID == crs.CrsID);

            oldcrs.CrsName = crs.CrsName;
            db.SaveChanges();

        }
    }
}
