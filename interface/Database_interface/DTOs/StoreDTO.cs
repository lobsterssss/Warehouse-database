using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal.DTOs
{
    public class StoreDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
        public string Street { get; set; }
    }
}
