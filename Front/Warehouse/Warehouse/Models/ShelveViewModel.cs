using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Warehouse_backend;

namespace Front_Warehouse.Models
{
    public class ShelveViewModel
    {
        public int? ID { get; set; }
        public String Name { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}
