using Front_Warehouse.Models;
using System.ComponentModel.DataAnnotations;

namespace Front_Warehouse.Models
{
    public class StoreViewModel
    {
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Postcode { get; set; }
        [Required]
        public String Street { get; set; }
        //public 
    }
}
