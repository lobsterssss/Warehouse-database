using Front_Warehouse.Models;
using InterfacesDal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WarehouseBLL;
using Warehouse_Dal;

namespace Front_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        private readonly Login LoginManager;

        public LoginController(Login LoginManager)
        {
            this.LoginManager = LoginManager;
        }

        [HttpGet("[controller]")]
        public IActionResult Login()
        {
            return View();
        }


        [BindProperty]
        public UserViewModel user { get; set; }

        [HttpPost("[controller]")]
        public async Task<ActionResult> LoginPost()
        {
            if (!ModelState.IsValid)
            {
                return View("login", user);
            }
            int? result = await LoginManager.Login_User(user.Name, user.Password);
            if (result != null)
            {
                HttpContext.Session.SetInt32("UserID", (int)result);
                return Redirect("/");
            }
            return View("login", user);
        }
    }
}
