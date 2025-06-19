using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Dal
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseConnection DatabaseConnection;
        public ProductRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }
        public async IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID)
        {
            MySqlCommand sqlcommend = new MySqlCommand(
                @"Select products.*, product_type.`Name`, product_type.`Description` from shelves
                inner JOIN products 
                    ON shelves.ID = products.Shelve_ID
                inner JOIN product_type 
                    ON products.Product_type_ID = product_type.ID
                where shelves.ID = @ID;");
            sqlcommend.Parameters.AddWithValue("@ID", ID);
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new ProductDTO
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Description = reader.GetString("Description"),
                    ProductCode = reader.GetString("Product_code"),
                    Amount = reader.GetInt32("Amount"),
                };
            }
        }

        public async IAsyncEnumerable<ProductDTO> GetAllProductsFromDelivery(int Id)
        {
            MySqlCommand sqlcommend = new MySqlCommand(
                @"Select delivery_products.*, product_type.*, products.Product_code  from deliveries
                INNER JOIN delivery_products 
                    ON deliveries.ID = delivery_products.delivery_ID
                INNER JOIN products 
                    ON products.ID = delivery_products.Product_ID
                INNER JOIN product_type 
                    ON product_type.ID = products.Product_type_ID
                WHERE deliveries.ID = @ID"
            );
            sqlcommend.Parameters.AddWithValue("@ID", Id);
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new ProductDTO
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Description = reader.GetString("Description"),
                    ProductCode = reader.GetString("Product_code"),
                    Amount = reader.GetInt32("Amount"),
                };
            }
        }
    }
}
