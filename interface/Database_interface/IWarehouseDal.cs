using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IWarehouseDal
    {
        public IAsyncEnumerable<DTOWarehouse> GetAllWarehouse();

        public IAsyncEnumerable<DTOWarehouse> GetWarehouse(int ID);

        public IAsyncEnumerable<int> CreateWarehouse(DTOWarehouse dTOWarehouse);

        public Task DeleteWarehouse(int ID);

        public Task UpdateWarehouse(DTOWarehouse dTOWarehouse);

    }
}
