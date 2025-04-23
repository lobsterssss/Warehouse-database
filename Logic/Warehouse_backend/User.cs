using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_backend
{
    public class User
    {
        private int ID;
        public string Name { get; private set; }
        public string Passcode { get; private set; }
        public Role Role { get; private set; }

        //private List<Warehouse> warehouses;


    }
}
