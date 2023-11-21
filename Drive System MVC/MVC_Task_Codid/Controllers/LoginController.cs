using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Task_Codid.Models;
using MVC_Task_Codid.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Task_Codid.Controllers
{
    public class LoginController : Controller
    {
        DriveDbContext dbContext = new DriveDbContext();
        public IActionResult Index()
        {
            return View("loginUser");
        }


        [HttpPost]

        public IActionResult loginUser(loginVM LogAccount)
        {
            User logger = new User();

            if (ModelState.IsValid)
            {

                var UserInput = dbContext.Users.Where(b => b.Email == LogAccount.Email && b.Password == LogAccount.Password).FirstOrDefault();

                if (UserInput != null)
                {
                    ClaimsIdentity claims = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme);
                    claims.AddClaim(new Claim(ClaimTypes.Email, UserInput.Email));
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserInput.UserId.ToString()));
                    ClaimsPrincipal principal = new ClaimsPrincipal(claims);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Files");


                }
                
                
                    ModelState.AddModelError("", "Username or Password Doesn't Exist");
                    return View("loginUser", LogAccount);

            }

            return View();

        }
        [Authorize]

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
