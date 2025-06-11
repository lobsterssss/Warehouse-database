using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc;
using WarehouseBLL;

namespace WarehousePresentation.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreCollection StoreCollection;

        public List<StoreViewModel> StoreViewModel = new List<StoreViewModel>();

        public StoreController(StoreCollection storeCollection)
        {
            this.StoreCollection = storeCollection;
        }
        public async Task<IActionResult> Index()
        {
            StoreViewModel = await this.StoreCollection.GetAllStores().Select(store => new StoreViewModel()
            {
                ID = store.ID,
                Name = store.Name,
                Postcode = store.Postcode,
                Street = store.Street,
            }).ToListAsync();
            return View(StoreViewModel);
        }
    }
}
