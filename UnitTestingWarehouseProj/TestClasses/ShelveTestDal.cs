using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_backend;

namespace UnitTestingWarehouseProj.TestClasses
{
    public class ShelveTestDal : IShelveRepository
    {
        private List<ShelveDTO> shelves = new List<ShelveDTO>();
        public ShelveTestDal()
        {
            shelves.Add(new ShelveDTO()
            {
                ID = 1,
                Name = "Shelve 1",
            });

            shelves.Add(new ShelveDTO()
            {
                ID = 2,
                Name = "Shelve 2",
            });
        }

        public IAsyncEnumerable<ShelveDTO> GetWarehouseShelves(int ID)
        {
            return shelves.Where(shelve => shelve.ID == ID).ToAsyncEnumerable();
        }
    }
}
