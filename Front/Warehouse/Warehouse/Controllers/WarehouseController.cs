using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Front_Warehouse.Models;
using Warehouse_backend;
using System.Threading.Tasks;

namespace Front_Front_Warehouse.Controllers
{
    [Route("/")]
    public class WarehouseController : Controller
    {
        private readonly WarehouseCollection warehouseCollection;

        public List<WarehouseViewModel> warehousesViewModel = new List<WarehouseViewModel>();

        public WarehouseController(WarehouseCollection warehouseCollection)
        {
            this.warehouseCollection = warehouseCollection;
        }

        public async Task<IActionResult> Index()
        {
            warehousesViewModel = await this.warehouseCollection.GetAllWarehouses().Select(warehouse => new WarehouseViewModel
            {
                ID = warehouse.ID,
                Name = warehouse.Name,
                Postcode = warehouse.Postcode,
                Street = warehouse.Street,
            }).ToListAsync();
            return View(warehousesViewModel);
        }

        [HttpGet("/{ID}/View")]
        public async Task<IActionResult> View(int ID)
        {
            Warehouse bllWarehouse = await this.warehouseCollection.GetWarehouse(ID);
            WarehouseViewModel warehousesViewModel = new WarehouseViewModel
            {
                ID = bllWarehouse.ID,
                Name = bllWarehouse.Name,
                Postcode = bllWarehouse.Postcode,
                Street = bllWarehouse.Street,
                Shelves = bllWarehouse.Shelves.Select(bllshelves => new ShelveViewModel
                {
                    ID = bllshelves.ID,
                    Name = bllshelves.Name,
                    Products = bllshelves.Products.Select(bllproducts => new ProductViewModel()
                    {
                        ID = bllproducts.ID,
                        Name = bllproducts.Name,
                        Description = bllproducts.Description,
                        ProductCode = bllproducts.ProductCode,
                        Amount = bllproducts.Amount,
                        
                    }).ToList(),
                }).ToList()
            };
            return View(warehousesViewModel);
        }
    }
}
