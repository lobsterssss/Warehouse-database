using System;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Warehouse_backend
{
    public static class UserCollection
    {
        public static async Task<List<User>> GetAllUsers(int ID) 
        {
            string json = await User_Dal.GetAll();
            List<User> users = new List<User>();
            users = JsonSerializer.Deserialize<List<User>>(json);
            return users;
        }
    }
}
