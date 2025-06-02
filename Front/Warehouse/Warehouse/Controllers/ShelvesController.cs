using InterfacesDal;
using Microsoft.AspNetCore.Mvc;
using Front_Warehouse.Models;
using WarehouseBLL;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Front_Front_Warehouse.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly ShelveCollection shelveCollection;

        public List<ShelveViewModel> shelvesViewModel = new List<ShelveViewModel>();

        public ShelvesController(ShelveCollection shelveCollection)
        {
            this.shelveCollection = shelveCollection;
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

        [HttpGet("/Warehouse/{warehouseID}/Shelves/Create")]
        public async Task<IActionResult> Create()
        {
            return View("ShelveForm");
        }

        [HttpPost("/Warehouse/{warehouseID}/Shelves/Create")]
        public async Task<IActionResult> CreatePost(string Name, int warehouseID)
        {
            int ShelveID = await this.shelveCollection.CreateShelve(Name, warehouseID);
            return Redirect($"/Shelves/{ShelveID}");
        }

        [HttpGet("/Warehouse/[controller]/{ID}/Edit")]
        public async Task<IActionResult> Edit(int ID)
        {
            Shelve shelve = await this.shelveCollection.GetShelve(ID);
            ShelveViewModel shelveViewModel = new ShelveViewModel
            {
                ID = shelve.ID,
                Name = shelve.Name,
            };
            return View("Shelveform", shelveViewModel);
        }

        [HttpPost("/Warehouse/[controller]/{ID}/Edit")]
        public async Task<IActionResult> EditPost(int ID, string Name)
        {
            Shelve Shelve = new Shelve(new ShelveRepository(), new ProductRepository())
            {
                ID = ID,
                Name = Name,
            };

            await Shelve.EditShelve();

            return Redirect($"/Shelves/{ID}");
        }
        [HttpPost("/Shelves/{ID}/Delete")]
        public async Task<IActionResult> Delete(int ID)
        {
            await shelveCollection.DeleteShelve(ID);

            return Redirect("/");
        }
    }
}
