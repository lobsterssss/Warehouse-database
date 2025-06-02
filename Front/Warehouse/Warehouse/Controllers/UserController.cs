using InterfacesDal;
using InterfacesDal.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Threading.Tasks;
using Front_Warehouse.Models;
using WarehouseBLL;
using Warehouse_Dal;

namespace Front_Warehouse.Controllers
{
    [Route("[controller]/")]
    public class UserController : Controller
    {
        private readonly UserCollection _userCollection;

        public UserController(UserCollection userCollection)
        {
            _userCollection = userCollection;
        }

        // GET: HomeController1
        [HttpGet]
        public ActionResult LoginView()
        {
            return View("login"); 
        }

        [HttpGet("index")]
        public ActionResult View()
        {
            return View("index");
        }


    }
}
