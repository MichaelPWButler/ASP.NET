using Microsoft.EntityFrameworkCore;
using Movies_Tutorial_Application.Data;
using Movies_Tutorial_Application.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Movies_Tutorial_ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Movies_Tutorial_ApplicationContext") ?? throw new InvalidOperationException("Connection string 'Movies_Tutorial_ApplicationContext' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
