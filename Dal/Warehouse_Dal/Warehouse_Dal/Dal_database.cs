using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Protocols;
using MySqlConnector;
using Newtonsoft.Json;
using System.Data;


namespace Warehouse_Dal
{
    public class Dal_database
    {
        public static async Task<String> Query(MySqlCommand Query)
        {
            string json = "";
            var connectionString = "Server=127.0.0.1;Port=3306;User ID=root;Password=root;Database=Warehouse;";

            try
            {
                await using var connection = new MySqlConnection(connectionString);
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                Query.Connection = connection;


                await connection.OpenAsync();


                DataTable dataTable = new DataTable();

                dataTable.Load(Query.ExecuteReader());

                json = JsonConvert.SerializeObject(dataTable);

                await connection.CloseAsync();

            }
            catch (MySqlException e)
            {
                Console.WriteLine($"SQL Error: {e.Message}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return json;

        }
    }
}

