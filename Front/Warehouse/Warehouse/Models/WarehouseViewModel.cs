﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class WarehouseViewModel
    {
        public int ID { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Postcode { get; set; }
        [Required]
        public String Street { get; set; }
        public List<ShelveViewModel> Shelves { get; set; }
    }
}
