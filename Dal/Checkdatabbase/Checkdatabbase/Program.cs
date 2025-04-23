// See https://aka.ms/new-console-template for more information
using Database_interface;
using System.Data.SqlClient;
using Warehouse_Dal;

namespace test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Iselect select = new User_Dal();
            await select.GetAll();

        }
    }
}
