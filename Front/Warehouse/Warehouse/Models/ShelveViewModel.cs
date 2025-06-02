using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class ShelveViewModel
    {
        public int? ID { get; set; }
        [MaxLength(20)]
        [MinLength(4)]
        [Required]
        public String Name { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}
