using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class WarehouseViewModel
    {
        public int ID { get; set; }
        [MinLength(4)]
        [Required]
        public String Name { get; set; }
        [MinLength(6)]
        [MaxLength(6)]
        [Required]
        public String Postcode { get; set; }
        [MinLength(2)]
        [Required]
        public String Street { get; set; }
        [ValidateNever]
        public List<ShelveViewModel> Shelves { get; set; }
    }
}
