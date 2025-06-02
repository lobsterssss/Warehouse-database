using InterfacesDal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IUserRepository
    {
        public IAsyncEnumerable<UserDTO> GetAll();

        public Task GetOne(int id);
        public Task GetWhere(int id, List<string> parameters);
        public Task GetOneWhere(int id, List<string> parameters);

    }
}
