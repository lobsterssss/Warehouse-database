using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front_Warehouse.Models
{
    public class DeliveryFormViewModel
    {
        public DeliveryViewModel DeliveryViewModel { get; set; }
        [ValidateNever]
        public WarehouseViewModel WarehouseViewModel { get; set; }

        public int selectedStore { get; set; }

        [ValidateNever]
        public List<SelectListItem> StoreViewModels { get; set; }
    }
}
