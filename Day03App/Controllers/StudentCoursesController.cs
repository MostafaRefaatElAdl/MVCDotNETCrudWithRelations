using Day03App.Models;
using Day04App.Data;
using Day04App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day04App.Controllers
{
    public class StudentCoursesController : Controller
    {
        private DatabaseITI _db;

        public StudentCoursesController(DatabaseITI db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.stds = new SelectList(_db.Students.ToList(), "Id", "Name");
            return View();
        }

        public IActionResult GetCourses(int id)
        {
            Student std = _db.Students.Include(x => x.Department).AsNoTracking().FirstOrDefault(s => s.Id == id);
            Department stdDpt = _db.Departments.Include(x => x.CoursesInDept).AsNoTracking().FirstOrDefault(x => x.Id == std.Department.Id);

            Student stdCrsToAdd = _db.Students.Include(x => x.StudentCourses).ThenInclude(x => x.Course).FirstOrDefault(x => x.Id == id);
            for (int i = 0; i < stdDpt.CoursesInDept.Count; i++)
            {
                stdCrsToAdd.StudentCourses.Add(new StudentCourses { StdId = std.Id, CrsId = stdDpt.CoursesInDept[i].CrsID });
            }
            stdCrsToAdd.StudentCourses = stdCrsToAdd.StudentCourses.DistinctBy(x => x.CrsId).ToList();
            _db.SaveChanges();
            List<StudentCourses> stdCrs = _db.Students.Include(x => x.StudentCourses).ThenInclude(x => x.Course).AsNoTracking().FirstOrDefault(x => x.Id == id).StudentCourses;

            return PartialView(stdCrs);
        }

        public IActionResult setGrade(int[] crsIDs, int[] Degrees, int id)
        {
            if (crsIDs.Length == Degrees.Length)
            {
                for (int i = 0; i < crsIDs.Length; i++)
                {
                    _db.StudentCourses.FirstOrDefault(x => x.StdId == id && x.CrsId == crsIDs[i]).Degree = Degrees[i];
                }
                _db.SaveChanges();
            }
            return RedirectToAction("index");
        }
    }
}
