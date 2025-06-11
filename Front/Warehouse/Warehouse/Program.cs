using Front_Warehouse.MiddelWare;
using InterfacesDal;
using WarehouseBLL;
using Warehouse_Dal;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserCollection>();
builder.Services.AddScoped<WarehouseCollection>();
builder.Services.AddScoped<ShelveCollection>();
builder.Services.AddScoped<Login>();

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
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
