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
    public class ProductTestDal : IProductRepository
    {
        private List<ProductDTO> Products = new List<ProductDTO>();
        public ProductTestDal() 
        {
            Products.Add(new ProductDTO()
            {
                ID = 1,
                Name = "beans",
                ProductCode = "G23XA",

            });
            Products.Add(new ProductDTO()
            {
                ID = 2,
                Name = "not Beans",
                ProductCode = "G23XA",
            });

        }
        public IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID)
        {
            return Products.Where(product => product.ID == ID).ToAsyncEnumerable();
        }
    }
}
