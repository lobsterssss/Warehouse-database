﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal.DTOs
{
    public class DeliveryDTO
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public List<ProductDTO> Products { get; set; }

    }
}
