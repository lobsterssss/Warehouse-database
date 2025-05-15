using Interfaces;
using MySqlConnector;
using System.Data;

namespace Warehouse_Dal
{
    public class Dal_database : IDatabaseDal
    {

        public static async Task<MySqlDataReader> SelectQuery(MySqlCommand Query)
        {
            var connectionString = "Server=127.0.0.1;Port=3306;User ID=root;Password=root;Database=Warehouse;";

                var connection = new MySqlConnection(connectionString);

                Query.Connection = connection;

                await connection.OpenAsync();

                MySqlDataReader reader = await Query.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                return reader;
        }

        public static async Task<int> InsertQuery(MySqlCommand Query)
        {
            var connectionString = "Server=127.0.0.1;Port=3306;User ID=root;Password=root;Database=Warehouse;";

            var connection = new MySqlConnection(connectionString);

            Query.Connection = connection;

            await connection.OpenAsync();

            return await Query.ExecuteNonQueryAsync();
        }
    }
}

