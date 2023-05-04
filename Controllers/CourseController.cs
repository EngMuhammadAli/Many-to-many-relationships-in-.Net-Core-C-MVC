using Microsoft.AspNetCore.Mvc;
using MvcMany.Models;

namespace MvcMany.Controllers
{
    public class CourseController : Controller
    {
        private readonly DataContext _dataContext;
        public CourseController(DataContext dataContext)
        {
                _dataContext=dataContext;
        }
        public IActionResult Index()
        {
            IList<Course> courses = _dataContext.Courses.ToList();
            return View(courses);
        }
        public IActionResult Details(int? Id)
        {
            Course course = _dataContext.Courses.SingleOrDefault(x => x.Id == Id);
            return View(course);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            _dataContext.Courses.Add(course);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _dataContext.Courses.Update(course);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int? Id)
        {
            Course course = _dataContext.Courses.SingleOrDefault(x => x.Id == Id);
            _dataContext.Remove(course);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
