using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DTOs
{
    public class DTOShelve
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<DTOProduct> Products { get; set; }

    }
}
