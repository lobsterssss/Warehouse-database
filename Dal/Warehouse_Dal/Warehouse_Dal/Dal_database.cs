using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Protocols;
using MySqlConnector;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection.PortableExecutable;


namespace Warehouse_Dal
{
    public class Dal_database
    {
        public static async Task<MySqlDataReader> Query(MySqlCommand Query)
        {
            string json = "";
            var connectionString = "Server=127.0.0.1;Port=3306;User ID=root;Password=root;Database=Warehouse;";

                await using var connection = new MySqlConnection(connectionString);

                Query.Connection = connection;

            using (connection.OpenAsync()) { 

                await using MySqlDataReader reader = await Query.ExecuteReaderAsync();

                return reader;
            }


        }
    }
}

