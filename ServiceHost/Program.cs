using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDo.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// خواندن connection string
var connectionString = builder.Configuration.GetConnectionString("ToDoListDB");

// کانفیگ وابستگی‌ها و EF Core
ToDoBootstrapper.Configure(builder.Services, connectionString);

// Razor Pages
builder.Services.AddRazorPages();
    //.AddRazorPagesOptions(options =>
    //{
    //    options.Conventions.AddAreaPageRoute("Adminpanel", "/ToDo/Tasks/Index", "Adminpanel/ToDo/Tasks");
    //});

// ساخت اپلیکیشن
var app = builder.Build();

// Middlewareها
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// تعریف endpointها
app.MapRazorPages();

// اجرای برنامه
app.Run();
