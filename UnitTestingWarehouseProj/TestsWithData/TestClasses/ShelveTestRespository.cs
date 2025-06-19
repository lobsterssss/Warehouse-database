using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestsWithData.TestClasses
{
    public class ShelveTestRespository : IShelveRepository
    {
        public ShelveDTO LastGivenDto;
        public int LastDeletedValue;
        public int WarehouseId;

        public async IAsyncEnumerable<int> CreateShelve(ShelveDTO dTOShelve, int warehouseID)
        {
            yield return 3;
        }

        public async Task DeleteShelve(int ID)
        {
            LastDeletedValue = ID;
        }

        public async IAsyncEnumerable<ShelveDTO> GetShelve(int ID)
        {
            yield return new ShelveDTO()
            {
                ID = 1,
                Name = "Shelve 1",
            };
        }

        public async IAsyncEnumerable<ShelveDTO> GetWarehouseShelves(int ID)
        {
            yield return new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
            };
            yield return new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
            };
            yield return new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
            };
            yield return new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
            };
        }

        public async Task UpdateShelve(ShelveDTO dTOShelve, int warehouseId)
        {
            LastGivenDto = dTOShelve;
            WarehouseId = warehouseId;
        }
    }
}
