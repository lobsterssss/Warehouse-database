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
    public class ShelveDal : IShelveDal
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
            using MySqlDataReader reader = await Daldatabase.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOShelve
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                };
            }
        }
        public async IAsyncEnumerable<DTOShelve> GetShelve(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from shelves where ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);
            using MySqlDataReader reader = await Daldatabase.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOShelve
                {
                    ID = reader.GetInt32("ID"),
                };
            }
        }
        public async IAsyncEnumerable<int> CreateShelve(DTOShelve dTOShelve, int warehouseID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"INSERT INTO shelves (Name, Warehouse_ID) VALUES(@Name, @WarehouseID);
            SELECT LAST_INSERT_ID() as 'ID';
            ");
            sqlcommend.Parameters.AddWithValue("@Name", dTOShelve.Name);
            sqlcommend.Parameters.AddWithValue("@WarehouseID", warehouseID);

            using MySqlDataReader reader = await Daldatabase.ReaderQuery(sqlcommend);

            while (await reader.ReadAsync())
            {
                yield return reader.GetInt32("ID");
            }
        }
        public async Task DeleteWarehouse(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"DELETE FROM shelves WHERE ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);

            await Daldatabase.ExecuteQuery(sqlcommend);

        }

        public async Task UpdateShelve(DTOShelve dTOShelve, int warehouseID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Update shelves set Name = @Name, Warehouse_ID = @WarehouseID where ID = @ID");
            sqlcommend.Parameters.AddWithValue("@ID", dTOShelve.ID);
            sqlcommend.Parameters.AddWithValue("@Name", dTOShelve.Name);
            sqlcommend.Parameters.AddWithValue("@WarehouseID", warehouseID);

            using MySqlDataReader reader = await Daldatabase.ReaderQuery(sqlcommend);

        }
    }
}
