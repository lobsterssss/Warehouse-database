using MySqlConnector;
using Warehouse_Dal;
namespace Front_Warehouse.MiddelWare
{
    public class DataConnMiddleWare
    {
        private readonly RequestDelegate _next;

        public DataConnMiddleWare(RequestDelegate next)
        {
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
                catch (MySqlException)
                {
                    context.Response.Redirect("/Error/500");
                    return;
                }
            }

            await _next(context);
        }
    }
}
