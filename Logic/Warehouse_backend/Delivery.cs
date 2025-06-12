using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class Delivery
    {
        private readonly IDeliveryRepository DeliveryRepository;

        public Delivery(IDeliveryRepository deliveryRepository)
        {
            DeliveryRepository = deliveryRepository;
        }

        public int ID { get; set; }
        public string Status { get; set; }
    }
}
