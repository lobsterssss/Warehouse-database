using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;
using static WarehouseBLL.Statuses;
using static WarehouseBLL.Role;

namespace UnitTestingWarehouseProj.TestsWithData.TestClasses
{
    public class LoginTestRespository : ILoginRepository
    {
        private List<ProductDTO> Products = new List<ProductDTO>();
        public LoginTestRespository() 
        {

        }

        public async IAsyncEnumerable<UserDTO> LoginRequest(string Name)
        {
            yield return new UserDTO() 
            {
                ID = 1,
                Name = Name,
                Passcode = "e7cf3ef4f17c3999a94f2c6f612e8a888e5b1026878e4e19398b23bd38ec221a",
                Role_ID = (int)Roles.worker,
            };
        }
    }
}
