using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData
{
    public class ProductTestRespository : IProductRepository
    {
        private List<ProductDTO> Products = new List<ProductDTO>();
        public ProductTestRespository() 
        {

        }

        public IAsyncEnumerable<ProductDTO> GetAllProductsFromDelivery(int Id)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID)
        {
            yield break;
        }
    }
}
