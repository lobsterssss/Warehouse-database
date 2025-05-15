using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Warehouse_backend;

namespace Front_Warehouse.Models
{
    [Serializable()]
    public class UserViewModel
    {
        public int? ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Password { get; set; }

        //public Role Role { get; private set; }

        //private List<Warehouse> warehouses;
    }
}
