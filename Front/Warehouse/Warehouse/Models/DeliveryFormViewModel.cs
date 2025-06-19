using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Front_Warehouse.Models
{
    public class DeliveryFormViewModel
    {
        public DeliveryViewModel DeliveryViewModel { get; set; } = new DeliveryViewModel();
        [ValidateNever]
        public WarehouseViewModel WarehouseViewModel { get; set; }

        [Required(ErrorMessage = "Please select a store.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a store.")]
        public int? selectedStore { get; set; }

        [ValidateNever]
        public List<SelectListItem> StoreViewModels { get; set; }
    }
}
