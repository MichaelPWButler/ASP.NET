var builder = WebApplication.CreateBuilder(args);

// Register HTTP client for Trading212
builder.Services.AddHttpClient("Trading212", client =>
{
    client.BaseAddress = new Uri("https://live.trading212.com"); // Change to live if needed
    client.DefaultRequestHeaders.Add("Authorization", "API_KEY HERE"); 
});

builder.Services.AddScoped<Trading212ApiService>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
