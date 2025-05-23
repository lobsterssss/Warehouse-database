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
    public class Shelve_Collection
    {
        private readonly IShelveDal ShelveDal;
        private readonly IProductDal ProductDal;


        public Shelve_Collection(IShelveDal shelveDal, IProductDal productDal)
        {
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }

        public async IAsyncEnumerable<Shelve> GetAllShelvesFromWarehouse(int warehouseID)
        {
            IAsyncEnumerable<DTOShelve> shelves = ShelveDal.GetWarehouseShelves(warehouseID);

            await foreach (DTOShelve shelve in shelves)
            {
                yield return new Shelve(ShelveDal, ProductDal)
                {
                    ID = shelve.ID,
                    Name = shelve.Name,
                };
            }
        }

        public async Task<Shelve?> GetShelve(int ID)
        {
            Shelve Shelve = new Shelve(ShelveDal, ProductDal);
            IAsyncEnumerable<DTOWarehouse> warehouses = ShelveDal.GetShelve(ID);

            await foreach (DTOWarehouse warehouse in warehouses)
            {
                Shelve = new Shelve(ShelveDal, ProductDal)
                {
                    ID = warehouse.ID,
                    Name = warehouse.Name,
                };
            }
            if (Shelve.Name == null)
            {
                return null;
            }
            await Shelve.GetProducts();


            return Shelve;
        }

        public async Task<int> CreateShelve(string name, int warehouseID)
        {
            DTOShelve Warehouse = new DTOShelve()
            {
                Name = name,
            };

            int WarehouseID = await ShelveDal.CreateShelve(Warehouse).FirstAsync();

            return WarehouseID;

        }

        public async Task DeleteWarehouse(int ID)
        {
            await ShelveDal.DeleteShelve(ID);
        }

    }
}
