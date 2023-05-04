namespace MvcMany.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<StudentCourses> StudentCourse { get; set; }
        public ICollection<TeacherCourse> TeacherCourse { get; set; }
    }
}
 