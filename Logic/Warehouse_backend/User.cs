using Interfaces.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Warehouse_backend
{
    public class User
    {
        private string Password;
        public int ID { get; set; }
        public string Name { get; set; }

        public Role? Role { get; set; }

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
