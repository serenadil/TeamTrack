using Microsoft.EntityFrameworkCore;
using TeamTrack.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Registra il DbContext con la configurazione centralizzata
builder.Services.AddTeamTrackDbContext(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

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
