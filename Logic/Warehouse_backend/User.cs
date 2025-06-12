using System.Security.Cryptography;
using System.Text;
using static WarehouseBLL.Role;

namespace WarehouseBLL
{
    public class User
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public Roles? Role { get; set; }

        public bool Login(string password)
        {
            return this.Password == HashString(password);
        }

        static string HashString(string input)
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
