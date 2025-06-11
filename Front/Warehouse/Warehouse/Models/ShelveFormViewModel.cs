using Front_Warehouse.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class ShelveFormViewModel
    {
        public ShelveViewModel shelve { get; set; }
        public string selectedWarehouse { get; set; }
        public List<SelectListItem> warehousesOptions { get; set; }
    }
}
