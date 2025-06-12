using InterfacesDal;

namespace WarehouseBLL
{
    public class Product
    {
        private readonly IProductRepository ProductDal;

        public Product(IProductRepository productDal)
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
