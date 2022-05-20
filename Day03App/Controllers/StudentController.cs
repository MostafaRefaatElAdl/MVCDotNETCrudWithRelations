using Day03App.Models;
using Day04App.Data;
using Day04App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Day04App.Controllers
{
    public class StudentController : Controller
    {
        IStudent db;
        IDepartment Ddb;
        SelectList depts;

        public StudentController(IStudent _db, IDepartment _Ddb)
        {
            db = _db;
            Ddb = _Ddb;
            depts = new(Ddb.GetAllDepartments(), "Id", "Name");
        }
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string userInput)
        {
            if (userInput == null || userInput == String.Empty)
            {
                List<Student> AllStudents = db.GetAllStudents();
                return View("Index", AllStudents);
            }
            List<Student> students = db.GetAllStudents().FindAll(x=>x.Name.ToLower().Contains(userInput.ToLower()));
            return View("Index", students);
        }
        public IActionResult Index()
        {
            return View(db.GetAllStudents());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            Student std = db.GetStudentById(id.Value);
            if (std == null)
                return NotFound();

            return View(std);
        }

        public IActionResult Create()
        {
            ViewBag.depts = depts;
            return View();
        }


        [HttpPost]
        public IActionResult Create(Student Student)
        {
            if (ModelState.IsValid)
            {
                db.AddStudent(Student);
                return RedirectToAction("Search", "Student");
            }
            else
            {
                return View(Student);
            }

        }

        public IActionResult Delete(int id)
        {
            return View(db.GetStudentById(id));
        }

        [HttpPost]
        [ActionName("delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            db.DeleteStudent(id);
            return RedirectToAction("Search");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.depts = depts;
            return View(db.GetStudentById(id));
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                db.UpdateStudent(std);
                return RedirectToAction("Search");
            }
            else
            {
                return View(std);
            }
        }

        public IActionResult CheckMailIfExc(string Email, int Id)
        {
            Student std = db.GetAllStudents().FirstOrDefault(x => x.Email == Email);
            Student stdID = db.GetAllStudents().FirstOrDefault(x => x.Id == Id);

                if (std == null || std==stdID)
                {
                    return Json(true);
                }
                else
                {
                    return Json("Email is Already There");
                }

        }
    }
}
