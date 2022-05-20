using Day03App.Models;
using Day04App.Models;
using Microsoft.EntityFrameworkCore;

namespace Day04App.Data
{
    public class DatabaseITI : DbContext
    {
        //static DatabaseITI dbMain=new DatabaseITI();
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourses> StudentCourses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=ASPMVCSystemLab;Trusted_Connection=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
        public DatabaseITI(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>().HasKey(x =>
            new { x.StdId, x.CrsId });

            modelBuilder.Entity<Course>().HasKey(x => x.CrsID);
            modelBuilder.Entity<Course>().Property(x => x.CrsName)
                .HasMaxLength(20)
                .IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
