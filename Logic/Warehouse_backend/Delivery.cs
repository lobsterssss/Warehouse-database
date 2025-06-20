using InterfacesDal;
using InterfacesDal.DTOs;
using System.Linq;

using static WarehouseBLL.Statuses;

namespace WarehouseBLL
{
    public class Delivery
    {
        private readonly IDeliveryRepository DeliveryRepository;
        private readonly IProductRepository ProductRepository;
        public Delivery(IDeliveryRepository deliveryRepository, IProductRepository productRepository)
        {
            DeliveryRepository = deliveryRepository;
            ProductRepository = productRepository;
        }

        public int ID { get; set; }
        public Status Status { get; set; }
        public List<Product> Products { get; set; }
        public async Task GetProducts()
        {
            Products = await ProductRepository.GetAllProductsFromDelivery(ID).Select(p => new Product(ProductRepository) 
            {
                Name = p.Name,
                Amount = p.Amount,
                Description = p.Description,
                ID = p.ID,
                ProductCode = p.ProductCode,
            
            }).ToListAsync();
        }
    }
}
