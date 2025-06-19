using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;
using Warehouse_Dal;
using static WarehouseBLL.Statuses;
using static WarehouseBLL.Role;

namespace UnitTestingWarehouseProj.TestsWithoutData.TestClasses
{
    public class LoginTestRespository : ILoginRepository
    {
        private List<ProductDTO> Products = new List<ProductDTO>();
        public LoginTestRespository() 
        {

        }

        public async IAsyncEnumerable<UserDTO> LoginRequest(string Name)
        {
            yield break;
        }
    }
}
