using Interfaces.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
     public interface IDatabaseDal
    {
        public static abstract Task<MySqlDataReader> SelectQuery(MySqlCommand Query);

        public static abstract Task<int> InsertQuery(MySqlCommand Query);

    }
}
