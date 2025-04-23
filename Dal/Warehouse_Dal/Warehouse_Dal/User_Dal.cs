using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_interface;
using MySqlConnector;

namespace Warehouse_Dal
{
    public class User_Dal
    {

        public static async Task<String> GetAll() 
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from users;");
            string json = await Dal_database.Query(sqlcommend);
            return json;
        }

        public Task GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetOneWhere(int id, List<string> parameters)
        {
            throw new NotImplementedException();
        }

        public Task GetWhere(int id, List<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
