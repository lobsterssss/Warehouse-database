using InterfacesDal;
using InterfacesDal.DTOs;
using WarehouseDal;

namespace WarehouseBLL
{
    public class DeliveryCollection
    {
        private readonly IDeliveryRepository DeliveryRepository;

        public DeliveryCollection(IDeliveryRepository deliveryRepository)
        {
            DeliveryRepository = deliveryRepository;
        }

        public async IAsyncEnumerable<Delivery> GetAllDeliveries(int userId, int warehouseId)
        {
            IAsyncEnumerable<DeliveryDTO> warehouses = DeliveryRepository.GetAllDeliveries(userId, warehouseId);

            await foreach (DeliveryDTO warehouse in warehouses)
            {
                yield return new Delivery(DeliveryRepository)
                {
                    ID = warehouse.ID,
                };
            }
        }

    }
}
