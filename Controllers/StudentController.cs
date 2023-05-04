using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMany.Models;
using MvcMany.Models.ViewModel;

namespace MvcMany.Controllers
{
    public class StudentController : Controller
    {
        private readonly DataContext _dataContext;
        public StudentController(DataContext dataContext)
        {
            _dataContext=dataContext;
        }
        public IActionResult Index()
        {
            IList<Student> students = _dataContext.Students.Include(x =>x.StudentCourse).ThenInclude(x => x.Course).ToList();
            return View(students);
        }
      
        public IActionResult Create()
        {
            var item = _dataContext.Courses.ToList();
            StudentCourseViewModel m1 = new StudentCourseViewModel();
            m1.AvailableCourses = item.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = false
            }).ToList();
            return View(m1);
        }

        [HttpPost]

        public IActionResult Create(StudentCourseViewModel SCVM , Student student ,StudentCourses St)
        {
            List<StudentCourses> stc = new List<StudentCourses>();
            student.Name = SCVM.Name;
            student.LastName = SCVM.LastName;
            student.RollNumber = SCVM.RollNumber;
            student.Email = SCVM.Email;
            _dataContext.Students.Add(student);
            _dataContext.SaveChanges();
            int studentid = student.Id;

            foreach (var item in SCVM.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    stc.Add(new StudentCourses() { StudentId = studentid, CourseId = item.Id });
                }
            }
            foreach (var item in stc)
            {
                _dataContext.StudentCourses.Add(item);
            }
            
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            StudentCourseViewModel SCVM = new StudentCourseViewModel();
            var st = _dataContext.Students.Include(x => x.StudentCourse).ThenInclude(e => e.Course).SingleOrDefault(m => m.Id == Id);
            var allcourse = _dataContext.Courses.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = vm.StudentCourse.Any(x => x.StudentId == st.Id) ? true : false,
            }).ToList();

            SCVM.Name = st.Name;
            SCVM.LastName = st.LastName;
            SCVM.Email = st.Email;
            SCVM.RollNumber = st.RollNumber;
            SCVM.AvailableCourses= allcourse;

            return View(SCVM);
        }

        [HttpPost]

        public IActionResult Edit(StudentCourseViewModel SCVM, Student student, StudentCourses St)
        {
            List<StudentCourses> stc = new List<StudentCourses>();
            student.Name = SCVM.Name;
            student.LastName = SCVM.LastName;
            student.RollNumber = SCVM.RollNumber;
            student.Email = SCVM.Email;
            _dataContext.Students.Update(student);
            _dataContext.SaveChanges();
            int studentid = student.Id;

            foreach (var item in SCVM.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    stc.Add(new StudentCourses() { StudentId = studentid, CourseId = item.Id });
                }
            }

            var databasetable = _dataContext.StudentCourses.Where(a => a.StudentId ==  studentid).ToList();
            var resultlist = databasetable.Except(stc).ToList();
            foreach (var item in resultlist)
            {
                _dataContext.StudentCourses.Remove(item);
                _dataContext.SaveChanges();
            }

            var getcourseid = _dataContext.StudentCourses.Where(x => x.StudentId == studentid).ToList();
            foreach (var item in stc)
            {
                if (!getcourseid.Contains(item))
                {
                    _dataContext.StudentCourses.Add(item);
                    _dataContext.SaveChanges();
                }
            }

           
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? Id)
        {
            Student student = _dataContext.Students.Include(x => x.StudentCourse).ThenInclude(x => x.Course).FirstOrDefault(x => x.Id == Id);
            return View(student);
        }

        public IActionResult Delete(int? Id)
        {
            Student student = _dataContext.Students.Include(x => x.StudentCourse).ThenInclude(x => x.Course).FirstOrDefault(x => x.Id == Id);
            return View(student);
        }
        [HttpPost]
        public IActionResult Delete(Student student)
        {
            _dataContext.Remove(student);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
