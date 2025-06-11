using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace WarehouseDal
{
    public class StoreRepository : IStoreRepository
    {
        public async IAsyncEnumerable<StoreDTO> GetAllStores()
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from stores");
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);

            while (await reader.ReadAsync())
            {
                yield return new StoreDTO
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Postcode = reader.GetString("Postcode"),
                    Street = reader.GetString("Street"),
                };
            }
        }

        public async IAsyncEnumerable<StoreDTO> GetStore(int iD)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from shelves where ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", iD);
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new StoreDTO
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
