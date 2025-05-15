using Front_Warehouse.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Warehouse_backend;
using Warehouse_Dal;

namespace Front_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        private readonly Login Login;

        public LoginController(Login login)
        {
            this.Login = login;
        }
        public IActionResult Index()
        {
            return View();
        }


        [BindProperty]
        public UserViewModel user { get; set; }

        [HttpPost("")]
        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return View("login", user);
            }
            int? result = await Login.Login_User(user.Name, user.Password);
            if (result != null)
            {
                user = new UserViewModel
                {
                    ID = result,
                    Name = user.Name,
                };
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));
                return Redirect("/");
            }

            return View("login", user);
        }
    }
}
