using Interfaces;
using Interfaces.DTOs;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Dal
{
    public class Warehouse_Dal : IWarehouseDal
    {

        public async IAsyncEnumerable<DTOWarehouse> GetAll() 
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from warehouses;");
            using MySqlDataReader reader = await Dal_database.SelectQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOWarehouse
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Postcode = reader.GetString("Postcode"),
                    Street = reader.GetString("Street"),
                };
            }

        }


        public async IAsyncEnumerable<DTOWarehouse> GetOne(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from warehouses where ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);
            using MySqlDataReader reader = await Dal_database.SelectQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOWarehouse
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Postcode = reader.GetString("Postcode"),
                    Street = reader.GetString("Street"),
                };
            }
        }


    }
}
