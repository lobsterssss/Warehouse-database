using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IProductRepository
    {
        public abstract IAsyncEnumerable<ProductDTO> GetShelveProducts(int ID);


    }
}
