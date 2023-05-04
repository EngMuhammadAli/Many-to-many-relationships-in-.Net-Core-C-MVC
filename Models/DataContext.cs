using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using MvcMany.Models.AccountModel;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MvcMany.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourses> StudentCourses { get; set; }

        public DbSet<Teacher> teachers { get; set; }

        public DbSet<TeacherCourse> TeachersCourses { get; set; }

        public DbSet<Department> departments { get; set; }

        public DbSet<RegisterModel> registerModel { get; set; }

        public DbSet<Role> roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Student course Many to Many
            builder.Entity<StudentCourses>().HasKey(cs => new { cs.StudentId, cs.CourseId });
            builder.Entity<StudentCourses>().HasOne(cd => cd.Student).WithMany(cd => cd.StudentCourse).HasForeignKey(cd => cd.StudentId);
            builder.Entity<StudentCourses>().HasOne(c => c.Course).WithMany(c => c.StudentCourse).HasForeignKey(c => c.CourseId);

            //Teacher course Many to Many
            builder.Entity<TeacherCourse>().HasKey(cc => new { cc.TeacherId, cc.CourseId });
            builder.Entity<TeacherCourse>().HasOne(dd => dd.Teacher).WithMany(dd => dd.TeacherCourses).HasForeignKey(dd => dd.TeacherId);



            // configures one-to-many relationship
            //builder.Entity<Semester>()
            //     .HasMany(c => c.Student)
            //         .WithOne(e => e.CurrentSemester).IsRequired();

            // // Global turn off delete behaviour on foreign keys
            // foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            // {
            //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
            // }
        }


    }
}
