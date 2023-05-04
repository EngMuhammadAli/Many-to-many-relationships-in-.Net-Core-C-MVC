namespace MvcMany.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RollNumber { get; set; }
        
        public ICollection<StudentCourses> StudentCourse { get; set; } = new HashSet<StudentCourses>();
    }
}
