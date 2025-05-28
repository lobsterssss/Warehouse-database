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
        private readonly ILoginRepository LoginDal;

        public Login(ILoginRepository LoginDal)
        {
            this.LoginDal = LoginDal;
        }

        public async Task<int?> Login_User(string Name, string password)
        {
            User User = new User();
            IAsyncEnumerable<UserDTO> users = LoginDal.LoginRequest(Name);

            await foreach (UserDTO user in users)
            {
                User = new User
                {
                    ID = User.ID,
                    Name = Name,
                    Password = user.Passcode
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
