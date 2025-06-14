﻿using InterfacesDal;
using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingWarehouseProj.TestClasses
{
    public class WarehouseTestRespository : IWarehouseRepository
    {
        public WarehouseDTO LastUpdatedDto;
        public async IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO dTOWarehouse)
        {
            yield return 3;
        }

        public async Task DeleteWarehouse(int ID)
        {

        }

        public async IAsyncEnumerable<WarehouseDTO> GetAllWarehouse()
        {
            yield return new WarehouseDTO()
            {
                ID = 1,
                Name = "warehouse 1",
                Postcode = "2132GS",
                Street = "street 1",
            };
            yield return new WarehouseDTO() {
                ID = 1,
                Name = "warehouse 1",
                Postcode = "2132GS",
                Street = "street 1",
            };
        }

        public async IAsyncEnumerable<WarehouseDTO> GetWarehouse(int ID)
        {
            yield return new WarehouseDTO()
            {
                ID = 1,
                Name = "warehouse 1",
                Postcode = "2132GS",
                Street = "street 1",
            };
        }

        public async Task UpdateWarehouse(WarehouseDTO dTOWarehouse)
        {
            LastUpdatedDto = dTOWarehouse;
        }
    }
}
