using Interfaces;
using Interfaces.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_backend;
using Warehouse_Dal;

namespace UnitTestingWarehouseProj.TestClasses
{
    public class ProductTestDal : IProductDal
    {
        private List<DTOProduct> Products = new List<DTOProduct>();
        public ProductTestDal() 
        {
            Products.Add(new DTOProduct()
            {
                ID = 1,
                Name = "beans",
                ProductCode = "G23XA",

            });
            Products.Add(new DTOProduct()
            {
                ID = 2,
                Name = "not Beans",
                ProductCode = "G23XA",
            });

        }
        public IAsyncEnumerable<DTOProduct> GetShelveProducts(int ID)
        {
            return Products.Where(product => product.ID == ID).ToAsyncEnumerable();
        }
    }
}
