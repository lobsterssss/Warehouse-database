using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesDal
{
     public interface IDatabaseConnection
    {
        public abstract Task<MySqlDataReader> ReaderQuery(MySqlCommand Query);
        public abstract Task<int> ExecuteQuery(MySqlCommand Query);
        public Task TestConn();
    }
}
