using InterfacesDal;
using Microsoft.AspNetCore.Mvc;
using Front_Warehouse.Models;
using WarehouseBLL;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Front_Warehouse.Controllers
{
    [Route("/")]
    public class WarehouseController : Controller
    {
        private readonly WarehouseCollection warehouseCollection;

        public WarehouseController(WarehouseCollection warehouseCollection)
        {
            this.warehouseCollection = warehouseCollection;

        }

        public async Task<IActionResult> Index()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            List<WarehouseViewModel> warehousesViewModel = await this.warehouseCollection.GetAllWarehouses(userID).Select(warehouse => new WarehouseViewModel
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
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            Warehouse bllWarehouse = await this.warehouseCollection.GetWarehouse(ID);
            if (bllWarehouse == null) 
            {
                return Redirect("Error/404");
            }
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

        [BindProperty]
        public WarehouseViewModel Warehouse { get; set; }

        [HttpPost("/Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View("WarehouseForm", Warehouse);
            }

            try
            {
                int userID = (int)HttpContext.Session.GetInt32("UserID");
                int warehouseID = await warehouseCollection.CreateWarehouse(Warehouse.Name, Warehouse.Postcode, Warehouse.Street);
                return Redirect($"/{warehouseID}/View");
            }
            catch (ArgumentException ex)
            {
                // Associate error with the correct field
                ModelState.AddModelError(ex.ParamName ?? "", ex.Message);
                return View("WarehouseForm", Warehouse);
            }
        }

        [HttpGet("/{ID}/Edit")]
        public async Task<IActionResult> Edit(int ID)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
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
        public async Task<IActionResult> EditPost()
        {
            if (!ModelState.IsValid)
            {
                return View("WarehouseForm", Warehouse);
            }
            Warehouse warehouse = new Warehouse(new WarehouseRepository(), new ShelveRepository(), new ProductRepository() ) { 
                ID = Warehouse.ID,
                Name = Warehouse.Name,
                Postcode= Warehouse.Postcode,
                Street = Warehouse.Street,
            };
            try
            {
                await warehouse.EditWarehouse(Warehouse.Name, Warehouse.Postcode, Warehouse.Street);
                return Redirect($"/{Warehouse.ID}/View");

            }
            catch (ArgumentException ex) 
            {
                ModelState.AddModelError(ex.ParamName ?? "", ex.Message);
                return View("WarehouseForm", Warehouse);
            }
        }

        [HttpPost("/{ID}/Delete")]
        public async Task<IActionResult> Delete(int ID)
        {
            await warehouseCollection.DeleteWarehouse(ID);

            return Redirect("/");
        }
    }
}
