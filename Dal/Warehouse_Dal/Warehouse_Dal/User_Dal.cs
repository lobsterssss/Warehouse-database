using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_interface;
using MySqlConnector;
using DTO_Warehouse;
using System.Data;

namespace Warehouse_Dal
{
    public static class User_Dal
    {

        public static async IAsyncEnumerable<DTOUser> GetAll() 
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from users;");
            MySqlDataReader reader = await Dal_database.Query(sqlcommend);
            while (await reader.ReadAsync()) 
            {
                yield return new DTOUser {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Role_ID = reader.GetInt32("Role_ID"),
                    Passcode = reader.GetString("Passcode")

                };
            }

        }


    }
}
