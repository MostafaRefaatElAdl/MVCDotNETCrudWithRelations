using Day04App.Data;

namespace Day03App.Models
{
    public interface IDepartment
    {
        public List<Department> GetAllDepartments();
        public Department GetDeparmentById(int id);
        public Department AddDepartment(Department dept);
        public void DeleteDepartment(int id);
        public void UpdateDepartment(Department dept);

    }
    public class DepartmentMoc:IDepartment
    {
        List<Department> _departments=new List<Department>()
        {
            new Department(){ Id = 100, Name = "OS", Location ="Mansoura"},
            new Department(){ Id = 200, Name = "SD", Location ="Alex"},
            new Department(){ Id = 300, Name = "MCP", Location ="Smart"}
        };

        public List<Department> GetAllDepartments()
        {
            return _departments;
        }

        public Department GetDeparmentById(int id)
        {
            return _departments.FirstOrDefault(x => x.Id == id);
        }

        public Department AddDepartment(Department dept)
        {
            _departments.Add(dept);
            return dept;
        }

        public void DeleteDepartment(int id)
        {
            _departments.Remove(_departments.FirstOrDefault(x => x.Id == id));
        }

        public void UpdateDepartment(Department dept)
        {
           Department oldDept = _departments.FirstOrDefault(x => x.Id == dept.Id);

            oldDept.Name = dept.Name;
            oldDept.Location = dept.Location;
        }
    }

    public class DepartmentDB : IDepartment
    {
        DatabaseITI db;
        public DepartmentDB(DatabaseITI db)
        {
            this.db = db;
        }

        public Department AddDepartment(Department dept)
        {
            db.Departments.Add(dept);
            db.SaveChanges();
            return dept;
        }

        public void DeleteDepartment(int id)
        {
            db.Departments.Remove(GetDeparmentById(id));
            db.SaveChanges();
        }

        public List<Department> GetAllDepartments()
        {
            return db.Departments.ToList();
        }

        public Department GetDeparmentById(int id)
        {
            return db.Departments.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateDepartment(Department dept)
        {
            Department oldDept = db.Departments.FirstOrDefault(x => x.Id == dept.Id);

            oldDept.Name = dept.Name;
            oldDept.Location = dept.Location;
            db.SaveChanges();

        }
    }
}
