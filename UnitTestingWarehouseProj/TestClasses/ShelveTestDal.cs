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
    public class ShelveTestDal : IShelveDal
    {
        private List<DTOShelve> shelves = new List<DTOShelve>();
        public ShelveTestDal()
        {
            shelves.Add(new DTOShelve()
            {
                ID = 1,
                Name = "Shelve 1",
            });

            shelves.Add(new DTOShelve()
            {
                ID = 2,
                Name = "Shelve 2",
            });
        }

        public IAsyncEnumerable<DTOShelve> GetWarehouseShelves(int ID)
        {
            return shelves.Where(shelve => shelve.ID == ID).ToAsyncEnumerable();
        }
    }
}
