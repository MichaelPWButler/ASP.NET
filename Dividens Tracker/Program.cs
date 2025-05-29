var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();              // For API controller
builder.Services.AddRazorPages();               // For UI
builder.Services.AddHttpContextAccessor();      // For accessing session in services
builder.Services.AddSession();                  // For user session

builder.Services.AddScoped<Trading212ApiService>(); // Our API wrapper

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();               // Required for session storage
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();           // Enables `[ApiController]` routing

app.Run();
