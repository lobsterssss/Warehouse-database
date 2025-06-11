using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
    public interface IStoreRepository
    {
        public IAsyncEnumerable<StoreDTO> GetAllStores();

        public IAsyncEnumerable<StoreDTO> GetStore(int iD);
    }
}
