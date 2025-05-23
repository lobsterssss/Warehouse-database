using Interfaces;
using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingWarehouseProj.TestClasses
{
    public class WarehouseTestDal : IWarehouseDal
    {
        private List<DTOWarehouse> warehouses = new List<DTOWarehouse>();
        public WarehouseTestDal() 
        {
            warehouses.Add(new DTOWarehouse()
            {
                ID = 1,
                Name = "warehouse 1",
                Postcode = "2132GS",
                Street = "street 1",
            });

            warehouses.Add(new DTOWarehouse()
            {
                ID = 2,
                Name = "warehouse 2",
                Postcode = "3132GS",
                Street = "street 2",
            });
        }


        public IAsyncEnumerable<int> CreateWarehouse(DTOWarehouse dTOWarehouse)
        {
            warehouses.Add(dTOWarehouse);
            dTOWarehouse.ID = warehouses.Count();
            warehouses.Last().ID = warehouses.Count();
            return new List<int>() { warehouses.Last().ID }.ToAsyncEnumerable();
        }

        public Task DeleteWarehouse(int ID)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<DTOWarehouse> GetAllWarehouse()
        {

            return warehouses.ToAsyncEnumerable();
        }

        public IAsyncEnumerable<DTOWarehouse> GetWarehouse(int ID)
        {
            return warehouses.Where(warehouse => warehouse.ID == ID).ToAsyncEnumerable();
        }

        public Task UpdateWarehouse(DTOWarehouse dTOWarehouse)
        {
            throw new NotImplementedException();
        }
    }
}
