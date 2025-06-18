using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    [Serializable()]
    public class UserViewModel
    {
        public int? ID { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string? Password { get; set; }

    }
}
