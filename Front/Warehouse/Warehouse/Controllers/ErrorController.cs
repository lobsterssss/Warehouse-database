using Microsoft.AspNetCore.Mvc;

namespace WarehousePresentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/[controller]/500")]
        public IActionResult Index()
        {
            return View("Error500");
        }
    }
}
