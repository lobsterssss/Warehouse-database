using InterfacesDal;
using InterfacesDal.DTOs;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Dal
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDatabaseConnection DatabaseConnection;
        public LoginRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }
        public async IAsyncEnumerable<UserDTO> LoginRequest(string Name)
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from users where Name = @Name;");
            sqlcommend.Parameters.AddWithValue("@Name", Name);
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new UserDTO
                {
                    ID = reader.GetInt32("ID"),
                    Passcode = reader.GetString("Passcode"),
                    Role_ID = reader.IsDBNull(reader.GetOrdinal("Role_ID")) ? (int?)null : reader.GetInt32("Role_ID"),
                };
            }
        }
    }
}
