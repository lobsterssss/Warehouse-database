using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Warehouse_backend;

namespace Front_Warehouse.Models
{
    public class ProductViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public int Amount { get; set; }
    }
}
