using Day03App.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Day04App.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(15, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        [Remote("CheckMailIfExc","Student",AdditionalFields ="Id")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }
        [Required] 
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public Student()
        {
            StudentCourses = new List<StudentCourses>();
        }
        // Relations >> Many students to one department
        public virtual Department Department { get; set; }
        // Relations >> Many Courses to Many students
        public virtual List<StudentCourses> StudentCourses { get; set; }

    }
}
