using Microsoft.AspNetCore.Mvc;

namespace WarehousePresentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/[controller]/404")]
        public IActionResult Index()
        {
            return View("Error404");
        }
    }
}
