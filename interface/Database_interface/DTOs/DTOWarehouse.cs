﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTOs
{
    public class DTOWarehouse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
        public string Street { get; set; }
        public List<DTOShelve> Shelves { get; set; }

    }
}
