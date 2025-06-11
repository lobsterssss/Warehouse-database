using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseBLL;

namespace UnitTestingWarehouseProj.TestClasses
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
            yield return new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
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

        public Task UpdateShelve(ShelveDTO dTOShelve)
        {
            throw new NotImplementedException();
        }
    }
}
