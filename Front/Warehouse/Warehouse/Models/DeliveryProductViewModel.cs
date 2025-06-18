using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class DeliveryProductViewModel
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ValidateNever]
        public string Description { get; set; }
        [ValidateNever]
        public string ProductCode { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
