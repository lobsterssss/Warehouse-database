using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_backend
{
    public class Shelve
    {
        private readonly IShelveDal ShelveDal;
        private readonly IProductDal ProductDal;

        public Shelve(IShelveDal shelveDal, IProductDal productDal)
        {
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }
        public int ID { get; set; }
        public String Name { get; set; }
        public List<Product> Products { get; set; }

        public async Task GetProducts()
        {
            var dTOProducts = await ProductDal.GetShelveProducts(this.ID).ToListAsync();
            Products = dTOProducts.Select(DTOproduct => new Product(ProductDal)
            {
                ID = DTOproduct.ID,
                Name = DTOproduct.Name,
                Description = DTOproduct.Description,
                ProductCode = DTOproduct.ProductCode,
                Amount = DTOproduct.Amount,

            }).ToList();
        }
    }
}
