using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class ShelveCollection
    {
        private readonly IShelveRepository ShelveDal;
        private readonly IProductRepository ProductDal;


        public ShelveCollection(IShelveRepository shelveDal, IProductRepository productDal)
        {
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }

        public async IAsyncEnumerable<Shelve> GetAllShelvesFromWarehouse(int warehouseID)
        {
            IAsyncEnumerable<ShelveDTO> shelves = ShelveDal.GetWarehouseShelves(warehouseID);

            await foreach (ShelveDTO shelve in shelves)
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
            IAsyncEnumerable<ShelveDTO> Shelves = ShelveDal.GetShelve(ID);

            await foreach (ShelveDTO shelve in Shelves)
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
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Shelve name is required", nameof(name));
            }
            if (warehouseID == null)
            {
                throw new ArgumentException("Warehouse is required", nameof(warehouseID));
            }

            ShelveDTO Shelve = new ShelveDTO()
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
