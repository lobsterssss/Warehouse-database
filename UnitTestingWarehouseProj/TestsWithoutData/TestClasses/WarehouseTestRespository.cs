using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData
{
    public class WarehouseTestRespository : IWarehouseRepository
    {
        public WarehouseDTO LastUpdatedDto;
        public int LastDeletedValue;
        public async IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO dTOWarehouse)
        {
            yield return 3;
        }

        public async Task DeleteWarehouse(int ID)
        {
            LastDeletedValue = ID;
        }

        public async IAsyncEnumerable<WarehouseDTO> GetAllWarehouse(int userId)
        {
            yield break;
        }

        public async IAsyncEnumerable<WarehouseDTO> GetWarehouse(int ID)
        {
            yield break;
        }

        public async Task UpdateWarehouse(WarehouseDTO dTOWarehouse)
        {
            LastUpdatedDto = dTOWarehouse;
        }
    }
}
