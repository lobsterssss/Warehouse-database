using Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IShelveDal
    {
        public abstract IAsyncEnumerable<DTOShelve> GetWarehouseShelves(int ID);

    }
}
