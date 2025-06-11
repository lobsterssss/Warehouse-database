using InterfacesDal;
using MySqlConnector;
using System.Data;

namespace Warehouse_Dal
{
    public class DatabaseConnection
    {
        private static string connectionString = "Server=127.0.0.1;Port=3306;User ID=root;Password=root;Database=Warehouse;";

        public static async Task<MySqlDataReader> ReaderQuery(MySqlCommand Query)
        {
                var connection = new MySqlConnection(connectionString);

                Query.Connection = connection;

                await connection.OpenAsync();

                MySqlDataReader reader = await Query.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                return reader;
        
        }

        public static async Task<int> ExecuteQuery(MySqlCommand Query)
        {
            var connection = new MySqlConnection(connectionString);

            Query.Connection = connection;

            await connection.OpenAsync();

            return await Query.ExecuteNonQueryAsync();
        }

        public static async Task TestConn()
        {
            var connection = new MySqlConnection(connectionString);


            await connection.OpenAsync();
            await connection.CloseAsync();
        }
    }
}

