using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;
using Warehouse_Dal;

namespace UnitTestingWarehouseProj.TestClasses
{
    public class ProductTestRespository : IProductRepository
    {
        private List<ProductDTO> Products = new List<ProductDTO>();
        public ProductTestRespository() 
        {

        }
        public async IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID)
        {
            yield return new ProductDTO()
            {
                ID = 2,
                Name = "beans",
                Amount = 26,
                Description = "test",
                ProductCode = "521sgh"
            };
        }
    }
}
