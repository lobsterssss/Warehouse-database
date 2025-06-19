using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace WarehouseDal
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly IDatabaseConnection DatabaseConnection;
        public DeliveryRepository(IDatabaseConnection databaseConnection) 
        {
            DatabaseConnection = databaseConnection;
        }

        public async Task<int> CreateDelivery(DeliveryDTO deliveryDTO, int warehouseId, int storeId)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"
            INSERT INTO deliveries
            (Store_ID, Warehouse_ID)
            VALUES(@Store_ID, @Warehouse_ID);
            SELECT LAST_INSERT_ID() as 'ID';
            ");
            sqlcommend.Parameters.AddWithValue("@Warehouse_ID", warehouseId);
            sqlcommend.Parameters.AddWithValue("@Store_ID", storeId);

            int deliveryId = 0;
            using (MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend))
            {
                while (await reader.ReadAsync())
                {
                    deliveryId = reader.GetInt32("ID");
                }
            }
            foreach (ProductDTO productDTO in deliveryDTO.Products)
            {
                sqlcommend = new MySqlCommand(@"
                INSERT INTO delivery_products
                (Delivery_ID, Product_ID, Amount)
                VALUES(@Delivery_ID, @Product_ID, @Amount);
            ");
                sqlcommend.Parameters.AddWithValue("@Delivery_ID", deliveryId);
                sqlcommend.Parameters.AddWithValue("@Product_ID", productDTO.ID);
                sqlcommend.Parameters.AddWithValue("@Amount", productDTO.Amount);

                await DatabaseConnection.ExecuteQuery(sqlcommend);

            }
            return deliveryId;
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

        public async IAsyncEnumerable<DeliveryDTO> GetDelivery(int iD)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"
                SELECT deliveries.*
                FROM deliveries
                Where ID = @ID
            ");
            sqlcommend.Parameters.AddWithValue("@ID", iD);

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

        public Task UpdateDelivery(DeliveryDTO deliveryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
