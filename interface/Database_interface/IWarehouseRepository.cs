using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IWarehouseRepository
    {
        public IAsyncEnumerable<WarehouseDTO> GetAllWarehouse();

        public IAsyncEnumerable<WarehouseDTO> GetWarehouse(int ID);

        public IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO dTOWarehouse);

        public Task DeleteWarehouse(int ID);

        public Task UpdateWarehouse(WarehouseDTO dTOWarehouse);

    }
}
