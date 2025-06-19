using InterfacesDal;
using InterfacesDal.DTOs;
using static WarehouseBLL.Role;

namespace WarehouseBLL
{
    public class Login
    {
        private readonly ILoginRepository LoginDal;

        public Login(ILoginRepository LoginDal)
        {
            this.LoginDal = LoginDal;
        }

        public async Task<User?> Login_User(string Name, string password)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("User name is required", nameof(Name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is required", nameof(password));
            }


            User User = new User();
            IAsyncEnumerable<UserDTO> users = LoginDal.LoginRequest(Name);

            await foreach (UserDTO user in users)
            {
                User = new User
                {
                    ID = user.ID,
                    Name = Name,
                    Password = user.Passcode,
                    Role = (Roles)user.Role_ID,
                };
            }
            if (User.Name == null || !User.Login(password))
            {
                return null;
            }
            return User;
        }
    }
}
