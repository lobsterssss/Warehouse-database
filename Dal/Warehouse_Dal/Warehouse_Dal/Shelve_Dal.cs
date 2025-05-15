using Interfaces;
using Interfaces.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Dal
{
    public class Shelve_Dal : IShelveDal
    {
        public async IAsyncEnumerable<DTOShelve> GetWarehouseShelves(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(
                @"Select shelves.* from warehouses
                INNER JOIN shelves 
                    ON warehouses.ID = shelves.Warehouse_ID
                WHERE warehouses.ID = @ID"
            );
            sqlcommend.Parameters.AddWithValue("@ID", ID);
            using MySqlDataReader reader = await Dal_database.SelectQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOShelve
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                };
            }
        }
    }
}
