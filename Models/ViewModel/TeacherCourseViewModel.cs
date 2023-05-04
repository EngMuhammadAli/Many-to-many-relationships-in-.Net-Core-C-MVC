using System.ComponentModel.DataAnnotations;

namespace MvcMany.Models.ViewModel
{
    public class TeacherCourseViewModel
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public IList<CheckBoxItem> AvailableCourses { get; set; }
    }
}
