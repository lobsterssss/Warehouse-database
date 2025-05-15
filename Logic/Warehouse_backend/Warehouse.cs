using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Dal;

namespace Warehouse_backend
{
    public class Warehouse
    {
        private readonly IWarehouseDal WarehouseDal;
        private readonly IShelveDal ShelveDal;
        private readonly IProductDal ProductDal;


        public Warehouse(IWarehouseDal warehouseDal, IShelveDal shelveDal, IProductDal productDal)
        {
            this.WarehouseDal = warehouseDal;
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }

        public int ID { get; set; }
        public String Name { get; set; }
        public String Postcode { get; set; }
        public String Street { get; set; }
        public List<Shelve> Shelves { get; set; }


        public async Task GetShelves() 
        {
            var DTOShelves = await ShelveDal.GetWarehouseShelves(this.ID).ToListAsync();
            this.Shelves = DTOShelves.Select(DTOShelves => new Shelve(ShelveDal, ProductDal) 
            {
                ID = DTOShelves.ID,
                Name = DTOShelves.Name,

            }).ToList();
        }

        public async Task GetProducts()
        {
            foreach (Shelve shelve in Shelves)
            {
               await shelve.GetProducts();
            }
        }
    }
}
