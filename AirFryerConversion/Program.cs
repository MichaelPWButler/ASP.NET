var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages services
builder.Services.AddRazorPages();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

// Optional: redirect root ("/") to Index page
// Not needed if page is Index.cshtml, because Razor Pages treats it as default

app.Run();
