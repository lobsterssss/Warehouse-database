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
        public abstract IAsyncEnumerable<DTOShelve> CreateShelve(DTOShelve dTOShelve ,int warehouseID);
        public abstract IAsyncEnumerable<DTOShelve> UpdateShelve(DTOShelve dTOShelve, int warehouseID);
        public abstract IAsyncEnumerable<DTOShelve> DeleteShelve(int ID);


    }
}
