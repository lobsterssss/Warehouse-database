using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc;
using WarehouseBLL;

namespace WarehousePresentation.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly DeliveryCollection DeliveryCollection;

        public List<DeliveryViewModel> DarehousesViewModel = new List<DeliveryViewModel>();

        public DeliveryController(DeliveryCollection deliveryCollection)
        {
            this.DeliveryCollection = deliveryCollection;
        }

        [HttpGet("/Warehouse/{warehouseID}/[controller]")]
        public async Task<IActionResult> IndexAsync(int warehouseID)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            DarehousesViewModel = await this.DeliveryCollection.GetAllDeliveries(userID, warehouseID).Select(warehouse => new DeliveryViewModel
            {
                ID = warehouse.ID,
            }).ToListAsync();
            return View(DarehousesViewModel);
        }
    }
}
