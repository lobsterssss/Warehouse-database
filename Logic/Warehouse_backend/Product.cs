using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_backend
{
    public class Product
    {
        private readonly IProductDal ProductDal;

        public Product(IProductDal productDal)
        {
            this.ProductDal = productDal;
        }
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public int Amount { get; set; }



    }
}
