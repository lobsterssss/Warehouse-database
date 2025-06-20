using InterfacesDal;
using InterfacesDal.DTOs;
using System.Security.Cryptography;
using System.Text;
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
            if (User.Name == null || !CheckPassword(User.Password, password))
            {
                return null;
            }
            return User;
        }

        private bool CheckPassword(string userPassword, string givenPassword)
        {
            return userPassword == HashString(givenPassword);
        }

        private static string HashString(string input)
        {
            //stolen from
            //https://www.geeksforgeeks.org/hash-function-for-string-data-in-c-sharp/
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hexadecimal string
                }
                return builder.ToString();
            }
        }
    }
}
