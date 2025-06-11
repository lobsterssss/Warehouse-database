using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IShelveRepository
    {
        public abstract IAsyncEnumerable<ShelveDTO> GetWarehouseShelves(int id);
        public abstract IAsyncEnumerable<ShelveDTO> GetShelve(int id);
        public abstract IAsyncEnumerable<int> CreateShelve(ShelveDTO dTOShelve ,int warehouseId);
        public abstract Task UpdateShelve(ShelveDTO dTOShelve, int warehouseId);
        public abstract Task DeleteShelve(int id);


    }
}
