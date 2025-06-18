using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class Shelve
    {
        private readonly IShelveRepository ShelveDal;
        private readonly IProductRepository ProductDal;

        public Shelve(IShelveRepository shelveDal, IProductRepository productDal)
        {
            this.ShelveDal = shelveDal;
            this.ProductDal = productDal;
        }
        public int ID { get; set; }
        public String Name { get; set; }
        public List<Product> Products { get; set; }

        public async Task EditShelve(int warehouseId)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentException("Shelve name is required", nameof(Name));
            }
            if (warehouseId != null)
            {
                throw new ArgumentException("Warehouse is required", nameof(warehouseId));
            }

            ShelveDTO dTOShelve = new ShelveDTO()
            {
                ID = this.ID,
                Name = this.Name,
            };
            await this.ShelveDal.UpdateShelve(dTOShelve, warehouseId);
        }

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
