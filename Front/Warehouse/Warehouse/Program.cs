using Front_Warehouse.MiddelWare;
using InterfacesDal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Warehouse_Dal;
using WarehouseBLL;
using WarehouseDal;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddDebug();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // default
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

DatabaseConnection.Initialize(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserCollection>();
builder.Services.AddScoped<WarehouseCollection>();
builder.Services.AddScoped<StoreCollection>();
builder.Services.AddScoped<DeliveryCollection>();
builder.Services.AddScoped<ShelveCollection>();
builder.Services.AddScoped<Login>();

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IShelveRepository, ShelveRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/html";

            var exceptionHandlerPathFeature =
                context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            var logger = context.RequestServices
                .GetRequiredService<ILogger<Program>>(); // or use a custom class name

            if (exceptionHandlerPathFeature?.Error is Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred at path: {Path}", exceptionHandlerPathFeature.Path);
            }

            context.Response.Redirect("/Error/500");
        });
    });

    app.UseHsts();
}

//app.UseAuthMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseMiddleware<DataConnMiddleWare>();
app.UseMiddleware<AuthMiddleWare>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
