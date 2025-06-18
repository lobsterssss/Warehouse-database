using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using WarehouseBLL;
using static WarehouseBLL.Statuses;

namespace Front_Warehouse.Models
{
    public class DeliveryViewModel
    {
        public int ID { get; set; }
        [ValidateNever]
        public Status Status { get; set; }
        public List<DeliveryProductViewModel> DeliveryProducts { get; set; }
    }
}
