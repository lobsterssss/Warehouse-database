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
    public class DeliveryRepository : IDeliveryRepository
    {
        public IAsyncEnumerable<int> CreateDelivery(DeliveryDTO deliveryDTO)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<DeliveryDTO> GetAllDeliveries(int userId, int warehouseID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"
                SELECT deliveries.*
                FROM deliveries
                INNER JOIN warehouses ON deliveries.Warehouse_ID = warehouses.ID
                WHERE (
                    warehouses.ID = @WarehouseID AND EXISTS (
                        SELECT 1
                        FROM users
                        INNER JOIN roles ON users.Role_ID = roles.ID
                        WHERE users.ID = @ID AND roles.Name = 'Admin'
                    )
                )
                OR (warehouses.ID = @WarehouseID and warehouses.ID IN (
                    SELECT user_warehouses.Warehouse_ID
                    FROM user_warehouses
                    WHERE user_warehouses.User_ID = @ID
                    )
                );
            ");
            sqlcommend.Parameters.AddWithValue("@ID", userId);
            sqlcommend.Parameters.AddWithValue("@WarehouseID", warehouseID);


            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DeliveryDTO
                {
                    ID = reader.GetInt32("ID"),
                    Status = reader.GetString("Status"),
                };
            }
        }

        public IAsyncEnumerable<DeliveryDTO> GetDelivery(int iD)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDelivery(DeliveryDTO deliveryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
