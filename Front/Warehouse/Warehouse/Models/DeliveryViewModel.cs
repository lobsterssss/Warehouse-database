using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class DeliveryViewModel
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public List<Product> DeliveryProducts { get; set; }
    }
}
