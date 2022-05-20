using Day03App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day03App.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartment db;

        public DepartmentController(IDepartment _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View(db.GetAllDepartments());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            Department dept = db.GetDeparmentById(id.Value);
            if (dept == null)
                return NotFound();

            return View(dept);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                db.AddDepartment(department);
                return RedirectToAction("Index", "Department");
            }
            else
            {
                return View(department);
            }

        }

        public IActionResult Delete(int id)
        {
            return View(db.GetDeparmentById(id));
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            db.DeleteDepartment(id);
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            return View(db.GetDeparmentById(id));
        }

        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                db.UpdateDepartment(dept);
                return RedirectToAction("index");
            }
            else
            {
                return View(dept);
            }
        }
    }
}
