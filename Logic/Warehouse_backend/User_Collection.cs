using Interfaces;
using Interfaces.DTOs;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Warehouse_backend
{
    public class UserCollection
    {
        private readonly IUserDal UserDal;

        public UserCollection(IUserDal userDal)
        {
            UserDal = userDal;
        }

        public async IAsyncEnumerable<User> GetAllUsers() 
        {
            IAsyncEnumerable<DTOUser> users = UserDal.GetAll();

            await foreach (DTOUser user in users)
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
