using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
    public interface IDeliveryRepository
    {
        public IAsyncEnumerable<DeliveryDTO> GetAllDeliveries(int userId, int warehouseID);

        public IAsyncEnumerable<DeliveryDTO> GetDelivery(int iD);

        public IAsyncEnumerable<int> CreateDelivery(DeliveryDTO deliveryDTO);

        public Task UpdateDelivery(DeliveryDTO deliveryDTO);
    }
}
