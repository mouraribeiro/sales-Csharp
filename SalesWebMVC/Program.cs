using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SalesWebMVC.Data;
var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(8, 0));
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMVCContext"), serverVersion));

// Add services to the container.
builder.Services.AddTransient<SeedingService>();
builder.Services.AddScoped<SeedingService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeedingService>();
        service.Seed();
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
   
}
app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
