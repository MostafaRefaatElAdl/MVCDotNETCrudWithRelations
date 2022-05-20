using Day04App.Data;
using Microsoft.EntityFrameworkCore;

namespace Day04App.Models
{
    public interface IStudent
    {
        public List<Student> GetAllStudents();
        public Student GetStudentById(int id);
        public Student AddStudent(Student std);
        public void DeleteStudent(int id);
        public void UpdateStudent(Student std);

    }
    public class StudentMoc:IStudent
    {
        List<Student> _Students = new List<Student>()
        {
            new Student(){ Id = 1, Name = "Mahmoud", Age =17},
            new Student(){ Id = 2, Name = "Bassem", Age =19},
            new Student(){ Id = 3, Name = "Ali", Age =18}
        };

        public List<Student> GetAllStudents()
        {
            return _Students;
        }

        public Student GetStudentById(int id)
        {
            return _Students.FirstOrDefault(x => x.Id == id);
        }

        public Student AddStudent(Student dept)
        {
            _Students.Add(dept);
            return dept;
        }

        public void DeleteStudent(int id)
        {
            _Students.Remove(_Students.FirstOrDefault(x => x.Id == id));
        }

        public void UpdateStudent(Student std)
        {
            Student oldDept = _Students.FirstOrDefault(x => x.Id == std.Id);

            oldDept.Name = std.Name;
            oldDept.Age = std.Age;
        }
    }

    public class StudentDB : IStudent
    {
        DatabaseITI db;
        public StudentDB(DatabaseITI db)
        {
            this.db = db;
        }
        public Student AddStudent(Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
            return std;
        }

        public void DeleteStudent(int id)
        {
            db.Students.Remove(GetStudentById(id));
            db.SaveChanges();
        }

        public List<Student> GetAllStudents()
        {
            return db.Students.Include(x=>x.Department).ToList();
        }

        public Student GetStudentById(int id)
        {
            return db.Students.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateStudent(Student std)
        {
            Student oldDept = db.Students.FirstOrDefault(x => x.Id == std.Id);

            oldDept.Name = std.Name;
            oldDept.Age = std.Age;
            oldDept.Password = std.Password;
            oldDept.DeptId = std.DeptId;
            db.SaveChanges();
        }
    }
}
