using Microsoft.EntityFrameworkCore;
using TrayInventoryApp.Data;
using TrayInventoryApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews(); // Add support for controllers with views

// Add session services
builder.Services.AddDistributedMemoryCache();  // Use in-memory cache for session data
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add DbContext and other services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<ITrayInventoryService, TrayInventoryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IDailyRateService, DailyRateService>();
builder.Services.AddScoped<ICostCalculation, CostCalculation>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Add UseSession before UseRouting
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Set default route for Admin/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}"); // Set Admin and Login as default

app.MapRazorPages(); // Keep Razor Pages mapping in case you need it

app.Run();
