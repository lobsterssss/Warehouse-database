﻿using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IProductDal
    {
        public abstract IAsyncEnumerable<DTOProduct> GetShelveProducts(int ID);


    }
}
