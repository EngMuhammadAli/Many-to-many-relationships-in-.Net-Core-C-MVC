using Microsoft.AspNetCore.Mvc;
using MvcMany.Models.ViewModel;
using MvcMany.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMany.Controllers
{
    public class TeacherController : Controller
    {
        private readonly DataContext _dataContext;
        public TeacherController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            IList<Teacher> teachers = _dataContext.teachers.Include(x => x.TeacherCourses).ThenInclude(s =>s.Course) .ToList();
            return View(teachers);
        }
        public IActionResult Create()
        {
            var item = _dataContext.Courses.ToList();
            TeacherCourseViewModel m1 = new TeacherCourseViewModel();
            m1.AvailableCourses = item.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = false
            }).ToList();
            return View(m1);
        }

        [HttpPost]

        public IActionResult Create(TeacherCourseViewModel SCVM, Teacher teacher, TeacherCourse St)
        {
            List<TeacherCourse> stc = new List<TeacherCourse>();
            teacher.Name = SCVM.Name;
            teacher.LastName = SCVM.LastName;
            teacher.HireDate = SCVM.HireDate;
           
            _dataContext.teachers.Add(teacher);
            _dataContext.SaveChanges();
            int treacherid = teacher.ID;

            foreach (var item in SCVM.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    stc.Add(new TeacherCourse() { TeacherId = treacherid, CourseId = item.Id });
                }
            }
            foreach (var item in stc)
            {
                _dataContext.TeachersCourses.Add(item);
            }

            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? Id)
        {
            TeacherCourseViewModel SCVM = new TeacherCourseViewModel();
            var st = _dataContext.teachers.Include(x => x.TeacherCourses).ThenInclude(e => e.Course).SingleOrDefault(m => m.ID == Id);
            var allcourse = _dataContext.Courses.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = vm.TeacherCourse.Any(x => x.TeacherId == st.ID) ? true : false,
            }).ToList();

            SCVM.Name = st.Name;
            SCVM.LastName = st.LastName;
            SCVM.HireDate = st.HireDate;
            SCVM.AvailableCourses = allcourse;

            return View(SCVM);
        }

        [HttpPost]

        public IActionResult Edit(TeacherCourseViewModel SCVM , Teacher teacher, TeacherCourse Ts)
        {
            List<TeacherCourse> stc = new List<TeacherCourse>();
            teacher.Name = SCVM.Name;
            teacher.LastName = SCVM.LastName;
            teacher.HireDate = SCVM.HireDate;
            _dataContext.teachers.Update(teacher);
            _dataContext.SaveChanges();
            int treacherid = teacher.ID;

            foreach (var item in SCVM.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    stc.Add(new TeacherCourse() { TeacherId = treacherid, CourseId = item.Id });
                }
            }

            var databasetable = _dataContext.TeachersCourses.Where(a => a.TeacherId == treacherid).ToList();
            var resultlist = databasetable.Except(stc).ToList();
            foreach (var item in resultlist)
            {
                _dataContext.TeachersCourses.Remove(item);
                _dataContext.SaveChanges();
            }

            var getcourseid = _dataContext.TeachersCourses.Where(x => x.TeacherId == treacherid).ToList();
            foreach (var item in stc)
            {
                if (!getcourseid.Contains(item))
                {
                    _dataContext.TeachersCourses.Add(item);
                    _dataContext.SaveChanges();
                }
            }


            return RedirectToAction("Index");
        }

        public IActionResult Details(int? Id)
        {
            Teacher teacher = _dataContext.teachers.Include(x => x.TeacherCourses).ThenInclude(x => x.Course).FirstOrDefault(x => x.ID == Id);
            return View(teacher);
        }

        public IActionResult Delete(int? Id)
        {
            Teacher teacher = _dataContext.teachers.Include(x => x.TeacherCourses).ThenInclude(x => x.Course).FirstOrDefault(x => x.ID == Id);
            return View(teacher);
        }
        [HttpPost]
        public IActionResult Delete(Teacher teacher)
        {
            _dataContext.Remove(teacher);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
