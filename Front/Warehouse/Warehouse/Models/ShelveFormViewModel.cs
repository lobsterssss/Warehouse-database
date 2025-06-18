using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class ShelveFormViewModel
    {
        [Required]
        public ShelveViewModel shelve { get; set; }
        [Required]
        public string selectedWarehouse { get; set; }
        [ValidateNever]
        public List<SelectListItem> warehousesOptions { get; set; }
    }
}
