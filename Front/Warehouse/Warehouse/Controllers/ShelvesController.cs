using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Front_Warehouse.Models;
using Warehouse_backend;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Front_Front_Warehouse.Controllers
{
    [Route("{WarehouseID}/shelves/")]
    public class ShelveController : Controller
    {
        private readonly WarehouseCollection warehouseCollection;

        public List<WarehouseViewModel> warehousesViewModel = new List<WarehouseViewModel>();

        public ShelveController(WarehouseCollection warehouseCollection)
        {
            this.warehouseCollection = warehouseCollection;
        }

        public async Task<IActionResult> Index(int WarehouseID)
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

        [HttpGet("/Create")]
        public async Task<IActionResult> Create()
        {
            return View("WarehouseForm");
        }

        public async Task<IActionResult> CreatePost(string Name )
        {
            int WarehouseID = await this.warehouseCollection.CreateWarehouse(Name);
            return Redirect($"/{WarehouseID}/View");
        }


        [HttpGet("/{ID}/Edit")]
        public async Task<IActionResult> Edit(int ID)
        {
            Warehouse warehouse = await this.warehouseCollection.GetWarehouse(ID);
            WarehouseViewModel warehousesViewModel = new WarehouseViewModel
            {
                ID = warehouse.ID,
                Name = warehouse.Name,
                Postcode = warehouse.Postcode,
                Street = warehouse.Street,
            };
            return View("WarehouseForm",warehousesViewModel);
        }

        [HttpPost("/{ID}/Edit")]
        public async Task<IActionResult> EditPost(int ID, string Name, string Postcode, string Street)
        {
           Warehouse warehouse = new Warehouse(new WarehouseDal(), new ShelveDal(), new ProductDal() ) { 
                ID = ID,
                Name = Name,
                Postcode= Postcode,
                Street = Street,
            };

            await warehouse.EditWarehouse(Name, Postcode, Street);
            
            return Redirect($"/{ID}/View");
        }

        [HttpGet("/{ID}/Delete")]
        public async Task<IActionResult> Delete(int ID)
        {
            await warehouseCollection.DeleteWarehouse(ID);

            return Redirect("/");
        }
    }
}
