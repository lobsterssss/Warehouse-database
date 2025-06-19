using Front_Warehouse.Models;
using InterfacesDal.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseBLL;

namespace WarehousePresentation.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly DeliveryCollection DeliveryCollection;
        private readonly WarehouseCollection warehouseCollection;
        private readonly StoreCollection StoreCollection;

        public List<DeliveryViewModel> DeliveriesViewModels = new List<DeliveryViewModel>();

        public DeliveryController(DeliveryCollection deliveryCollection, WarehouseCollection warehouseCollection, StoreCollection storeCollection)
        {
            this.StoreCollection = storeCollection;
            this.warehouseCollection = warehouseCollection;
            this.DeliveryCollection = deliveryCollection;
        }

        private static WarehouseViewModel MapWarehouseToWareHouseViewModel(Warehouse bllWarehouse)
        {
            return new WarehouseViewModel
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
        }


        [HttpGet("/Warehouse/{warehouseID}/[controller]")]
        public async Task<IActionResult> IndexAsync(int warehouseID)
        {
            int userID = (int)HttpContext.Session.GetInt32("UserID");
            DeliveriesViewModels = await this.DeliveryCollection.GetAllDeliveries(userID, warehouseID).Select(warehouse => new DeliveryViewModel
            {
                ID = warehouse.ID,
            }).ToListAsync();
            ViewBag.warehouseID = warehouseID;
            return View(DeliveriesViewModels);
        }

        [HttpGet("/Warehouse/{warehouseID}/[controller]/create")]
        public async Task<IActionResult> Create(int warehouseID)
        {
            DeliveryFormViewModel deliveryFormViewModel = new DeliveryFormViewModel();
            Warehouse bllWarehouse = await this.warehouseCollection.GetWarehouse(warehouseID);
            if (bllWarehouse == null)
            {
                return Redirect("Error/404");
            }
            deliveryFormViewModel.WarehouseViewModel = MapWarehouseToWareHouseViewModel(bllWarehouse);

            deliveryFormViewModel.StoreViewModels = await this.StoreCollection.GetAllStores().Select(store => new SelectListItem
            {
                Value = store.ID.ToString(),
                Text = store.Name,
            }).ToListAsync();


            return View("DeliveryForm", deliveryFormViewModel);
        }

        [HttpPost("/Warehouse/{warehouseID}/[controller]/create")]
        public async Task<IActionResult> CreatePost(int warehouseID, DeliveryFormViewModel deliveryFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                Warehouse bllWarehouse = await this.warehouseCollection.GetWarehouse(warehouseID);
                if (bllWarehouse == null)
                {
                    return Redirect("Error/404");
                }
                deliveryFormViewModel.WarehouseViewModel = MapWarehouseToWareHouseViewModel(bllWarehouse);

                deliveryFormViewModel.StoreViewModels = await this.StoreCollection.GetAllStores().Select(store => new SelectListItem
                {
                    Value = store.ID.ToString(),
                    Text = store.Name,
                }).ToListAsync();

                return View("DeliveryForm", deliveryFormViewModel);
            }

            DeliveryDTO deliveryDTO = new DeliveryDTO()
            {
                Products = deliveryFormViewModel.DeliveryViewModel.DeliveryProducts.Select(product => new ProductDTO()
                {
                    ID = product.ID,
                    Amount = product.Amount,
                }).ToList()
            };
            try
            {
                int deliveryId = await DeliveryCollection.CreateDelivery(warehouseID, (int)deliveryFormViewModel.selectedStore, deliveryDTO, warehouseCollection);
            }
            catch (Exception)
            {

                Warehouse bllWarehouse = await this.warehouseCollection.GetWarehouse(warehouseID);
                if (bllWarehouse == null)
                {
                    return Redirect("Error/404");
                }
                deliveryFormViewModel.WarehouseViewModel = MapWarehouseToWareHouseViewModel(bllWarehouse);

                deliveryFormViewModel.StoreViewModels = await this.StoreCollection.GetAllStores().Select(store => new SelectListItem
                {
                    Value = store.ID.ToString(),
                    Text = store.Name,
                }).ToListAsync();

                return View("DeliveryForm", deliveryFormViewModel);
            }
            return Redirect($"/Warehouse/{warehouseID}/Delivery");
        }

        [HttpGet("/[controller]/{deliveryID}/view")]
        public async Task<IActionResult> View(int deliveryID)
        {
            Delivery delivery = await DeliveryCollection.GetDelivery(deliveryID);
            DeliveryViewModel deliveryView = new DeliveryViewModel()
            {
                ID = delivery.ID,
                Status = delivery.Status,
                DeliveryProducts = delivery.Products.Select(p => new DeliveryProductViewModel()
                {
                    ID = p.ID,
                    Amount = p.Amount,
                    Name = p.Name,
                    ProductCode = p.ProductCode,
                    Description = p.Description,

                }).ToList()


            };
            return View(deliveryView);
        }
    }
}
