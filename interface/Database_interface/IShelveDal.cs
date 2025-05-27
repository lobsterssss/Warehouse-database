using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IShelveDal
    {
        public abstract IAsyncEnumerable<DTOShelve> GetWarehouseShelves(int ID);
        public abstract IAsyncEnumerable<DTOShelve> GetShelve(int ID);
        public abstract IAsyncEnumerable<int> CreateShelve(DTOShelve dTOShelve ,int warehouseID);
        public abstract Task UpdateShelve(DTOShelve dTOShelve);
        public abstract Task DeleteShelve(int ID);


    }
}
