using InterfacesDal;
using InterfacesDal.DTOs;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("UnitTestingWarehouseProj")]
namespace WarehouseBLL
{
    public class WarehouseCollection
    {
        private readonly IWarehouseRepository WarehouseDal;
        private readonly IShelveRepository ShelveDal;
        private readonly IProductRepository ProductDal;

        public WarehouseCollection(IWarehouseRepository warehouseDal, IShelveRepository shelveDal, IProductRepository productDal)
        {
            this.WarehouseDal = warehouseDal;
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }

        public async IAsyncEnumerable<Warehouse> GetAllWarehouses(int userId)
        {
            IAsyncEnumerable<WarehouseDTO> warehouses = WarehouseDal.GetAllWarehouse(userId);

            await foreach (WarehouseDTO warehouse in warehouses)
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
            IAsyncEnumerable<WarehouseDTO> warehouses = WarehouseDal.GetWarehouse(ID);

            await foreach (WarehouseDTO warehouse in warehouses)
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Warehouse name is required", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(postcode))
            {
                throw new ArgumentException("Postcode is required", nameof(postcode));
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Street is required", nameof(street));
            }

            WarehouseDTO Warehouse = new WarehouseDTO()
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
