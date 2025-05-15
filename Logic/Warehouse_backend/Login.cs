using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_backend
{
    public class Login
    {
        private readonly ILoginDal LoginDal;

        public Login(ILoginDal LoginDal)
        {
            this.LoginDal = LoginDal;
        }

        public async Task<int?> Login_User(string Name, string password)
        {
            User User = new User();
            IAsyncEnumerable<DTOUser> users = LoginDal.LoginRequest(Name);

            await foreach (DTOUser user in users)
            {
                User = new User
                {
                    ID = User.ID,
                    Name = Name,
                    //Role = user.Role_ID,
                };
            }
            if (User.Name == null || !User.Login(password))
            {
                return null;
            }
            return User.ID;
        }
    }
}
