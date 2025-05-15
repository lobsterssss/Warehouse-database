using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Warehouse_backend;

namespace Front_Warehouse.Models
{
    public class WarehouseViewModel
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Postcode { get; set; }
        public String Street { get; set; }
        public List<ShelveViewModel> Shelves { get; set; }
    }
}
