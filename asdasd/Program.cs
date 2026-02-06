using Microsoft.EntityFrameworkCore;
using asdasd.Data;
using asdasd.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация на услугите

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 2. Автоматично създаване на базата и данни (seeding)
using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Геймърска мишка", Price = 45.99m },
            new Product { Name = "Механична клавиатура", Price = 120.0m },
            new Product { Name = "Монитор Zowie", Price = 350.50m }
        ); 
        context.SaveChanges();
    }
}

// 3. Конфигурация на HTTP пайплайна
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

    