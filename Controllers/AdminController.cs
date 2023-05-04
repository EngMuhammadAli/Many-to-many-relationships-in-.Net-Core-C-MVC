using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMany.Models;
using MvcMany.Models.AccountModel;

namespace MvcMany.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _dataContext;
        public AdminController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            IList<RegisterModel> registerModelist = _dataContext.registerModel.Include(x => x.Role).ToList();
            return View(registerModelist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Role = new SelectList(_dataContext.roles.Where(x => x.Name == "Admin"), "Id", "Name").ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(RegisterModel registerModel)
        {
            registerModel.AccessToken = Guid.NewGuid().ToString();
            _dataContext.registerModel.Add(registerModel);
            _dataContext.SaveChanges();

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.UtcNow.AddDays(10);
            Response.Cookies.Append("user-access-token", registerModel.AccessToken);
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(RegisterModel registerModel)
        {
            RegisterModel registerModel1 = _dataContext.registerModel.Where(x => x.Email.Equals(registerModel.Email) && x.Password.Equals(registerModel.Password)).FirstOrDefault();
            if (registerModel1 == null)
            {
                ViewBag.Error = "Email Or Passwor Is Not Correct";
                return View();
            }
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.UtcNow.AddDays(10);
            Response.Cookies.Append("user-access-token", registerModel1.AccessToken);
            return Redirect("/Home/Index");

        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user-access-token");
            return Redirect("/Home/Index");
        }
    }
}
