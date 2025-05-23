using Interfaces;
using Interfaces.DTOs;
using System;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Warehouse_Dal;
[assembly: InternalsVisibleTo("UnitTestingWarehouseProj")]
namespace Warehouse_backend
{
    public class WarehouseCollection
    {
        private readonly IWarehouseDal WarehouseDal;
        private readonly IShelveDal ShelveDal;
        private readonly IProductDal ProductDal;



        public WarehouseCollection(IWarehouseDal warehouseDal, IShelveDal shelveDal, IProductDal productDal )
        {
            this.WarehouseDal = warehouseDal;
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }

        public async IAsyncEnumerable<Warehouse> GetAllWarehouses() 
        {
            IAsyncEnumerable<DTOWarehouse> warehouses = WarehouseDal.GetAllWarehouse();

            await foreach (DTOWarehouse warehouse in warehouses)
            {
                yield return new Warehouse(WarehouseDal, ShelveDal, ProductDal)
                {
                    ID = warehouse.ID,
                    Name = warehouse.Name,
                    Postcode = warehouse.Postcode,
                    Street = warehouse.Street,
                };
            }
        }

        public async Task<Warehouse?> GetWarehouse(int ID)
        {
            Warehouse Warehouse = new Warehouse(WarehouseDal, ShelveDal, ProductDal);
            IAsyncEnumerable<DTOWarehouse> warehouses = WarehouseDal.GetWarehouse(ID);

            await foreach (DTOWarehouse warehouse in warehouses)
            {
                Warehouse = new Warehouse(WarehouseDal, ShelveDal, ProductDal)
                {
                    ID = warehouse.ID,
                    Name = warehouse.Name,
                    Postcode = warehouse.Postcode,
                    Street = warehouse.Street,
                };
            }
            if (Warehouse.Name == null)
            {
                return null;
            }
            await Warehouse.GetShelves();
            Warehouse.Shelves = Warehouse.Shelves ?? [];
            await Warehouse.GetProducts();


            return Warehouse;
        }

        public async Task<int> CreateWarehouse(string name, string postcode, string street)
        {
            DTOWarehouse Warehouse = new DTOWarehouse() 
            {
               Name = name,
               Postcode = postcode,
               Street = street,
            };

            int WarehouseID = await WarehouseDal.CreateWarehouse(Warehouse).FirstAsync();

            return WarehouseID;

        }

        public async Task DeleteWarehouse(int ID)
        {
            await WarehouseDal.DeleteWarehouse(ID);
        }

    }
}
