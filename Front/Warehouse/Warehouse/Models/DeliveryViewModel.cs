using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WarehouseBLL;
using static WarehouseBLL.Statuses;

namespace Front_Warehouse.Models
{
    public class DeliveryViewModel
    {
        public int ID { get; set; }
        [ValidateNever]
        public Status Status { get; set; }
        [Required( ErrorMessage = "Please add a product with a valid amount.")]
        public List<DeliveryProductViewModel> DeliveryProducts { get; set; }
    }
}
