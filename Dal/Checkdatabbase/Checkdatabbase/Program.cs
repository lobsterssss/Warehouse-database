// See https://aka.ms/new-console-template for more information
using Database_interface;
using System.Data.SqlClient;
using Warehouse_Dal;
using DTO_Warehouse;

namespace test
{
    class Program
    {
        static async Task Main(string[] args)
        {

            List<string> stink = await User_Dal.GetAll();

            foreach (string item in stink)
            {
                Console.WriteLine(item);
            }
        }
    }
}
