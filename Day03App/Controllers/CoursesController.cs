using Day04App.Models;
using Microsoft.AspNetCore.Mvc;


namespace Day04App.Controllers
{
    public class CourseController : Controller
    {
        ICourse db;

        public CourseController(ICourse _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.GetAllCourses());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            Course crs = db.GetCourseById(id.Value);
            if (crs == null)
                return NotFound();

            return View(crs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course Course)
        {
            if (ModelState.IsValid)
            {
                db.AddCourse(Course);
                return RedirectToAction("Index", "Course");
            }
            else
            {
                return View(Course);
            }

        }

        public IActionResult Delete(int id)
        {
            return View(db.GetCourseById(id));
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            db.DeleteCourse(id);
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            return View(db.GetCourseById(id));
        }

        [HttpPost]
        public IActionResult Edit(Course crs)
        {
            if (ModelState.IsValid)
            {
                db.UpdateCourse(crs);
                return RedirectToAction("index");
            }
            else
            {
                return View(crs);
            }
        }
    }
}
