using Microsoft.EntityFrameworkCore;
using TeamTrack.Infrastrutture; // Assicurati che il namespace sia corretto

var builder = WebApplication.CreateBuilder(args);

// Configura il DbContext per usare SQL Server
builder.Services.AddDbContext<TeamTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddHttpClient();

// Aggiungi altri servizi, come i controller
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura il middleware
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
    pattern: "{controller=Utente}/{action=Login}/{id?}");

app.Run();
