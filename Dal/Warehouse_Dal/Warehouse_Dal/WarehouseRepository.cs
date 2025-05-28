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
    public class WarehouseRepository : IWarehouseRepository
    {

        public async IAsyncEnumerable<WarehouseDTO> GetAllWarehouse() 
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from warehouses;");
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new WarehouseDTO
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Postcode = reader.GetString("Postcode"),
                    Street = reader.GetString("Street"),
                };
            }

        }


        public async IAsyncEnumerable<WarehouseDTO> GetWarehouse(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from warehouses where ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new WarehouseDTO
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Postcode = reader.GetString("Postcode"),
                    Street = reader.GetString("Street"),
                };
            }
        }

        public async IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO dTOWarehouse)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"
            INSERT INTO warehouses
            (Name, Postcode, Street)
            VALUES(@Name, @Postcode, @Street);
            SELECT LAST_INSERT_ID() as 'ID';
            ");
            sqlcommend.Parameters.AddWithValue("@Name", dTOWarehouse.Name);
            sqlcommend.Parameters.AddWithValue("@Postcode", dTOWarehouse.Postcode);
            sqlcommend.Parameters.AddWithValue("@Street", dTOWarehouse.Street);

            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);

            while (await reader.ReadAsync())
            {
                yield return reader.GetInt32("ID");
            }
        }
        public async Task DeleteWarehouse(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"DELETE FROM warehouses WHERE ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);

            await DatabaseConnection.ExecuteQuery(sqlcommend);

        }

        public async Task UpdateWarehouse(WarehouseDTO dTOWarehouse)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"
            Update warehouses
            set Name = @Name, Postcode = @Postcode, Street = @Street
            where ID = @ID"
            );
            sqlcommend.Parameters.AddWithValue("@ID", dTOWarehouse.ID);
            sqlcommend.Parameters.AddWithValue("@Name", dTOWarehouse.Name);
            sqlcommend.Parameters.AddWithValue("@Postcode", dTOWarehouse.Postcode);
            sqlcommend.Parameters.AddWithValue("@Street", dTOWarehouse.Street);

            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);

        }

    }
}
