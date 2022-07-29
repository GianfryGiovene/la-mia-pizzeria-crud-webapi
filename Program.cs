using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LaMiaPizzeria.Models.Repositories.Interfaces;
using LaMiaPizzeria.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPizzaRepository, InMemoryPizzaRepository>();

var connectionString = builder.Configuration.GetConnectionString("PizzaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzaContextConnection' not found.");
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlServer(connectionString)); 

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PizzaContext>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();




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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
