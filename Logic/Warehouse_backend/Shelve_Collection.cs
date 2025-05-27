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
    public class ShelveCollection
    {
        private readonly IShelveDal ShelveDal;
        private readonly IProductDal ProductDal;


        public ShelveCollection(IShelveDal shelveDal, IProductDal productDal)
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
            IAsyncEnumerable<DTOShelve> Shelves = ShelveDal.GetShelve(ID);

            await foreach (DTOShelve shelve in Shelves)
            {
                Shelve = new Shelve(ShelveDal, ProductDal)
                {
                    ID = shelve.ID,
                    Name = shelve.Name,
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
            DTOShelve Shelve = new DTOShelve()
            {
                Name = name,
            };

            int ShelveID = await ShelveDal.CreateShelve(Shelve, warehouseID).FirstAsync();

            return ShelveID;

        }

        public async Task DeleteShelve(int ID)
        {
            await ShelveDal.DeleteShelve(ID);
        }

    }
}
