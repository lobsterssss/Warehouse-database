using InterfacesDal;
using InterfacesDal.DTOs;
using System.Linq;
using WarehouseDal;
using static WarehouseBLL.Statuses;

namespace WarehouseBLL
{
    public class DeliveryCollection
    {
        private readonly IDeliveryRepository DeliveryRepository;
        private readonly IProductRepository ProductRepository;


        public DeliveryCollection(IDeliveryRepository deliveryRepository , IProductRepository productRepository)
        {
            DeliveryRepository = deliveryRepository;
            ProductRepository = productRepository;
        }

        public async IAsyncEnumerable<Delivery> GetAllDeliveries(int userId, int warehouseId)
        {
            IAsyncEnumerable<DeliveryDTO> warehouses = DeliveryRepository.GetAllDeliveries(userId, warehouseId);

            await foreach (DeliveryDTO warehouse in warehouses)
            {
                yield return new Delivery(DeliveryRepository, ProductRepository)
                {
                    ID = warehouse.ID,
                };
            }
        }

        public async Task<int> CreateDelivery(int warehouseId, int storeId, DeliveryDTO deliveryDTO, WarehouseCollection warehouseCollection)
        {
            Warehouse warehouse = await warehouseCollection.GetWarehouse(warehouseId);

            foreach (ProductDTO product in deliveryDTO.Products)
            {
                foreach (Shelve shelves in warehouse.Shelves)
                {
                    foreach (Product warehouseProduct in shelves.Products)
                    {
                        if (warehouseProduct.ID == product.ID) 
                        {
                            if (warehouseProduct.Amount < product.Amount) 
                            {
                                throw new Exception();
                            }
                        }
                    }
                }
            }

            int deliveryID = await DeliveryRepository.CreateDelivery(deliveryDTO, warehouseId, storeId);

            return deliveryID;
        }
        public async Task<Delivery?> GetDelivery(int ID)
        {
            Delivery Delivery = new Delivery(DeliveryRepository, ProductRepository);
            IAsyncEnumerable<DeliveryDTO> deliverys = DeliveryRepository.GetDelivery(ID);

            await foreach (DeliveryDTO delivery in deliverys)
            {
                Delivery = new Delivery(DeliveryRepository, ProductRepository)
                {
                    ID = delivery.ID,  
                    Status = (Status)Enum.Parse(typeof(Status), delivery.Status),
                };
            }
            if (Delivery == null)
            {
                return null;
            }
            await Delivery.GetProducts();


            return Delivery;
        }

    }
}
