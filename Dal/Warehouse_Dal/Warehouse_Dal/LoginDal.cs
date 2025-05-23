using Interfaces;
using Interfaces.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Dal
{
    public class LoginDal : ILoginDal
    {
        public async IAsyncEnumerable<DTOUser> LoginRequest(string Name)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from users where Name = @Name;");
            sqlcommend.Parameters.AddWithValue("@Name", Name);
            using MySqlDataReader reader = await Daldatabase.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new DTOUser
                {
                    ID = reader.GetInt32("ID"),
                    Role_ID = reader.IsDBNull(reader.GetOrdinal("Role_ID")) ? (int?)null : reader.GetInt32("Role_ID"),
                };
            }
        }
    }
}
