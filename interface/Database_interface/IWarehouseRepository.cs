﻿using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IWarehouseRepository
    {
        public IAsyncEnumerable<WarehouseDTO> GetAllWarehouse(int userId);

        public IAsyncEnumerable<WarehouseDTO> GetWarehouse(int iD);

        public IAsyncEnumerable<int> CreateWarehouse(WarehouseDTO warehouseDTO);

        public Task DeleteWarehouse(int ID);

        public Task UpdateWarehouse(WarehouseDTO warehouseDTO);

    }
}
