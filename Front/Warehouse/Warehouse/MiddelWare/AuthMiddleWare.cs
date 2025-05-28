namespace Front_Warehouse.MiddelWare
{
    public class AuthMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Example: Check if the user is authenticated via session
            var isAuthenticated = context.Session.GetInt32("UserID") != null;

            if (!isAuthenticated && !context.Request.Path.StartsWithSegments("/Login"))
            {
                // Redirect to login if not authenticated
                context.Response.Redirect("/Login");
                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
