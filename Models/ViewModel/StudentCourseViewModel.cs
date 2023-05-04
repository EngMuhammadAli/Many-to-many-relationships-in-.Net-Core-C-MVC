using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
namespace MvcMany.Models.ViewModel
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string RollNumber { get; set; }

        public List<CheckBoxItem> AvailableCourses { get; set; }
    }
}
