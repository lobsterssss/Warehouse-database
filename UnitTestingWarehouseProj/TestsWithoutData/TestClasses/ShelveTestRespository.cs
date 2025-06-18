using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithoutData.TestClassesWithoutData
{
    public class ShelveTestRespository : IShelveRepository
    {
        public ShelveDTO LastUpdatedDto;

        public IAsyncEnumerable<int> CreateShelve(ShelveDTO dTOShelve, int warehouseID)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteShelve(int ID)
        {
            
        }

        public async IAsyncEnumerable<ShelveDTO> GetShelve(int ID)
        {
            yield break;
        }

        public async IAsyncEnumerable<ShelveDTO> GetWarehouseShelves(int ID)
        {
            yield break;
        }

        public Task UpdateShelve(ShelveDTO dTOShelve, int warehouseId)
        {
            throw new NotImplementedException();
        }
    }
}
