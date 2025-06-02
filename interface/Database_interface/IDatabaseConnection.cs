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
        public static abstract Task<MySqlDataReader> ReaderQuery(MySqlCommand Query);

        public static abstract Task<int> ExecuteQuery(MySqlCommand Query);

    }
}
