using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesDal;
using MySqlConnector;
using System.Data;
using InterfacesDal.DTOs;

namespace Warehouse_Dal
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnection DatabaseConnection;
        public UserRepository(IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
        }
        public async IAsyncEnumerable<UserDTO> GetAll() 
        {
            MySqlCommand sqlcommend = new MySqlCommand(@"Select * from users;");
            using MySqlDataReader reader = await DatabaseConnection.ReaderQuery(sqlcommend);
            while (await reader.ReadAsync())
            {
                yield return new UserDTO 
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    Role_ID = reader.IsDBNull(reader.GetOrdinal("Role_ID")) ? (int?)null : reader.GetInt32("Role_ID"),
                    Passcode = reader.GetString("Passcode"),
                };
            }
        }

        public Task GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetOneWhere(int id, List<string> parameters)
        {
            throw new NotImplementedException();
        }

        public Task GetWhere(int id, List<string> parameters)
        {
            throw new NotImplementedException();
        }


    }
}
