using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IProductRepository
    {
        public abstract IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID);


    }
}
