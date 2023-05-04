using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcMany.Models;
using MvcMany.Models.AccountModel;

namespace MvcMany.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        public AccountController(DataContext data)
        {
            _dataContext = data;
        }
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            return RedirectToAction("Register");
        }
    }
}
