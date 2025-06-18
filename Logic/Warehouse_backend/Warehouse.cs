using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class Warehouse
    {
        private readonly IWarehouseRepository WarehouseDal;
        private readonly IShelveRepository ShelveDal;
        private readonly IProductRepository ProductDal;


        public Warehouse(IWarehouseRepository warehouseDal, IShelveRepository shelveDal, IProductRepository productDal)
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

        public async Task EditWarehouse(string name, string postcode, string street)
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

            WarehouseDTO dTOWarehouse = new WarehouseDTO()
            {
                ID = this.ID,
                Name = name,
                Postcode = postcode,
                Street = street,
            };
            await this.WarehouseDal.UpdateWarehouse(dTOWarehouse);
        }

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
