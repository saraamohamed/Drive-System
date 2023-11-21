
using Microsoft.AspNetCore.Mvc;
using MVC_Task_Codid.Models;
using MVC_Task_Codid.ViewModels;

namespace MVC_Task_Codid.Controllers
{
    public class RegisterController : Controller
    {
        DriveDbContext dbContext = new DriveDbContext();

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegistrationView()
        {
            return View("RegistrationView");

        }
        public IActionResult AddAccount(RegisterVM NewAccount)
        {
            ModelState.Clear();
            if (HttpContext.Request.Method == "POST")
            {
                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.FirstName = NewAccount.FirstName;
                    user.LastName = NewAccount.LastName;
                    user.Email = NewAccount.Email;
                    user.Password = NewAccount.Password;
                    user.ConfirmPassword = NewAccount.ConfirmPassword;
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();                
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return View(RegistrationView);

                }

            }
            return View(RegistrationView);
        }
    }
}
