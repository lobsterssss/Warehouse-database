using Front_Warehouse.Models;
using InterfacesDal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Warehouse_Dal;
using WarehouseBLL;

namespace Front_Warehouse.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly ShelveCollection shelveCollection;
        private readonly WarehouseCollection warehouseCollection;

        public List<ShelveViewModel> shelvesViewModel = new List<ShelveViewModel>();

        public ShelvesController(ShelveCollection shelveCollection, WarehouseCollection warehouseCollection)
        {
            this.shelveCollection = shelveCollection;
            this.warehouseCollection = warehouseCollection;

        }

        [HttpGet("/Warehouse/{warehouseID}/shelves")]
        public async Task<IActionResult> Index(int warehouseID)
        {
            shelvesViewModel = await this.shelveCollection.GetAllShelvesFromWarehouse(warehouseID).Select(shelve => new ShelveViewModel
            {
                ID = shelve.ID,
                Name = shelve.Name,
            }).ToListAsync();
            return View(shelvesViewModel);
        }

        [HttpGet("/[controller]/{ID}")]
        public async Task<IActionResult> View(int ID)
        {
            Shelve bllShelve = await this.shelveCollection.GetShelve(ID);
            if (bllShelve == null)
            {
                return View("Error404");
            }
            ShelveViewModel shelveViewModel = new ShelveViewModel
                {
                    ID = bllShelve.ID,
                    Name = bllShelve.Name,
                    Products = bllShelve.Products.Select(bllproducts => new ProductViewModel()
                    {
                        ID = bllproducts.ID,
                        Name = bllproducts.Name,
                        Description = bllproducts.Description,
                        ProductCode = bllproducts.ProductCode,
                        Amount = bllproducts.Amount,
                        
                    }).ToList(),
            };
            return View(shelveViewModel);
        }


        [HttpGet("/Shelves/Create")]
        public async Task<IActionResult> Create()
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ShelveFormViewModel ShelveForm = new ShelveFormViewModel();

            ShelveForm.warehousesOptions = await this.warehouseCollection.GetAllWarehouses(userID).Select(warehouse => new SelectListItem
            {
                Value = warehouse.ID.ToString(),
                Text = warehouse.Name,
            }).ToListAsync();


            return View("ShelveForm", ShelveForm);
        }

        [HttpPost("/Shelves/Create")]
        public async Task<IActionResult> CreatePost(ShelveFormViewModel ShelveForm)
        {
            if (!ModelState.IsValid)
            {
                return await Create();
            }

            int ShelveID = await this.shelveCollection.CreateShelve(ShelveForm.shelve.Name, int.Parse(ShelveForm.selectedWarehouse));
            return Redirect($"/Shelves/{ShelveID}");
        }

        [HttpGet("/[controller]/{ID}/Edit")]
        public async Task<IActionResult> Edit(int ID)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            ShelveFormViewModel ShelveForm = new ShelveFormViewModel();

            ShelveForm.warehousesOptions = await this.warehouseCollection.GetAllWarehouses(userID).Select(warehouse => new SelectListItem
            {
                Value = warehouse.ID.ToString(),
                Text = warehouse.Name,
            }).ToListAsync();

            Shelve shelve = await this.shelveCollection.GetShelve(ID);
            ShelveForm.shelve = new ShelveViewModel
            {
                ID = shelve.ID,
                Name = shelve.Name,
            };
            return View("Shelveform", ShelveForm);
        }

        [HttpPost("/[controller]/{ID}/Edit")]
        public async Task<IActionResult> EditPost(int ID,ShelveFormViewModel ShelveForm)
        {
            if (!ModelState.IsValid)
            {
                return await Edit(ID);
            }
            Shelve Shelve = new Shelve(new ShelveRepository(), new ProductRepository())
            {
                ID = ID,
                Name = ShelveForm.shelve.Name,
            };
            try
            {
                await Shelve.EditShelve(int.Parse(ShelveForm.selectedWarehouse));
                return Redirect($"/Shelves/{ID}");
            }
            catch (ArgumentException ex)
            {
                // Associate error with the correct field
                ModelState.AddModelError(ex.ParamName ?? "", ex.Message);
                return await Edit(ID);

            }
        }

        [HttpPost("/Shelves/{ID}/Delete")]
        public async Task<IActionResult> Delete(int ID)
        {
            await shelveCollection.DeleteShelve(ID);

            return Redirect("/");
        }
    }
}
