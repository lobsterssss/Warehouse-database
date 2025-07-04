﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarehouseBLL;

namespace Front_Warehouse.Models
{
    public class ShelveViewModel
    {
        public int? ID { get; set; }
        [MaxLength(20)]
        [MinLength(2)]
        [Required]
        public String Name { get; set; }
        [ValidateNever]
        public List<ProductViewModel> Products { get; set; }

    }
}
