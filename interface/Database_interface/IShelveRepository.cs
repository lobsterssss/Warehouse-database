using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IShelveRepository
    {
        public abstract IAsyncEnumerable<ShelveDTO> GetWarehouseShelves(int ID);
        public abstract IAsyncEnumerable<ShelveDTO> GetShelve(int ID);
        public abstract IAsyncEnumerable<int> CreateShelve(ShelveDTO dTOShelve ,int warehouseID);
        public abstract Task UpdateShelve(ShelveDTO dTOShelve);
        public abstract Task DeleteShelve(int ID);


    }
}
