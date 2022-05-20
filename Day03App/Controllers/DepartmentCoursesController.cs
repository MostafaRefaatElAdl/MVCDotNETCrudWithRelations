using Day03App.Models;
using Day04App.Data;
using Day04App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day04App.Controllers
{
    public class DepartmentCoursesController : Controller
    {
        private DatabaseITI _db;

        public DepartmentCoursesController(DatabaseITI db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.depts = new SelectList(_db.Departments.ToList(), "Id", "Name");
            return View();
        }

        public IActionResult GetCourses(int id)
        {
            Department dept = _db.Departments.Include(x=>x.CoursesInDept).FirstOrDefault(x => x.Id == id);

            List<Course> coursesInDept = dept.CoursesInDept;
            List<Course> coursesNotInDept = _db.Courses.ToList().Except(coursesInDept).ToList();

            DepartmentCourseViewModel model = new DepartmentCourseViewModel();
            model.CoursesInDept = coursesInDept;
            model.CoursesNotInDept = coursesNotInDept;
            model.Department = dept;

            return PartialView(model);
        }

        public IActionResult updateCourses(int[] coursesInDept, int[] coursesNotInDept, int id)
        {
            Department dept = _db.Departments.Include(x=>x.CoursesInDept).FirstOrDefault(x => x.Id == id);
            List<Course> coursesToRemove= new List<Course>();
            foreach (var item in coursesInDept)
            {
                coursesToRemove.Add(_db.Courses.FirstOrDefault(x => x.CrsID == item));
            }
            foreach (var item in coursesToRemove)
            {
                dept.CoursesInDept.Remove(item);
            }
            _db.SaveChanges();

            List<Course> coursesToAdd = new List<Course>();
            foreach (var item in coursesNotInDept)
            {
                coursesToAdd.Add(_db.Courses.FirstOrDefault(x => x.CrsID == item));
            }
            foreach (var item in coursesToAdd)
            {
                dept.CoursesInDept.Add(item);
            }
            _db.SaveChanges();


            return RedirectToAction("index");
        }
    }
}
