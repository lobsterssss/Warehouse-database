using Interfaces;
using Warehouse_backend;
using Warehouse_Dal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserDal, User_Dal>();
builder.Services.AddScoped<UserCollection>();

builder.Services.AddScoped<IWarehouseDal, Warehouse_Dal.Warehouse_Dal>();
builder.Services.AddScoped<IShelveDal, Shelve_Dal>();
builder.Services.AddScoped<IProductDal, Product_Dal>();

builder.Services.AddScoped<WarehouseCollection>();
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
