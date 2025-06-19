using InterfacesDal;
using MySqlConnector;
using NLog;
using Warehouse_Dal;
namespace Front_Warehouse.MiddelWare
{
    public class DataConnMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly IDatabaseConnection DatabaseConnection;

        public DataConnMiddleWare(RequestDelegate next, IDatabaseConnection databaseConnection)
        {
            DatabaseConnection = databaseConnection;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/Error"))
            {
                try
                {
                    await DatabaseConnection.TestConn();
                }
                catch (MySqlException e)
                {
                    context.Response.Redirect("/Error/500");
                    return;
                }
            }

            await _next(context);
        }
    }
}
