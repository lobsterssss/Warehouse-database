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
    public class WarehouseTestDal : IWarehouseRepository
    {
        private List<WarehouseDTO> warehouses = new List<WarehouseDTO>();
        public WarehouseTestDal() 
        {
            warehouses.Add(new WarehouseDTO()
            {
                ID = 1,
                Name = "warehouse 1",
                Postcode = "2132GS",
                Street = "street 1",
            });

            warehouses.Add(new WarehouseDTO()
            {
                ID = 2,
                Name = "warehouse 2",
                Postcode = "3132GS",
                Street = "street 2",
            });
        }


        public IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO dTOWarehouse)
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

        public IAsyncEnumerable<WarehouseDTO> GetAllWarehouse()
        {

            return warehouses.ToAsyncEnumerable();
        }

        public IAsyncEnumerable<WarehouseDTO> GetWarehouse(int ID)
        {
            return warehouses.Where(warehouse => warehouse.ID == ID).ToAsyncEnumerable();
        }

        public Task UpdateWarehouse(WarehouseDTO dTOWarehouse)
        {
            throw new NotImplementedException();
        }
    }
}
