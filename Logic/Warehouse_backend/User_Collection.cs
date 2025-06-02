using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace WarehouseBLL
{
    public class UserCollection
    {
        private readonly IUserRepository UserDal;

        public UserCollection(IUserRepository userDal)
        {
            UserDal = userDal;
        }

        public async IAsyncEnumerable<User> GetAllUsers() 
        {
            IAsyncEnumerable<UserDTO> users = UserDal.GetAll();

            await foreach (UserDTO user in users)
            {
                yield return new User() 
                {
                    ID = user.ID,
                    Name = user.Name,
                    //Role = user.Role_ID

                };
            }
        }
    }
}
