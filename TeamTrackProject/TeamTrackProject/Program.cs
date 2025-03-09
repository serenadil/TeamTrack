using Microsoft.EntityFrameworkCore;
using TeamTrackProject.Models.Infrastrutture;
using TeamTrackProject.Models.Repo;
using TeamTrackProject.Models.Servizi;

var builder = WebApplication.CreateBuilder(args);

// Recupera la stringa di connessione
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Aggiungi servizi al contenitore.
builder.Services.AddControllersWithViews();

// Registra il DbContext con SQL Server
builder.Services.AddDbContext<TeamTrackDbContext>(options =>
    options.UseSqlServer(connectionString));

// Aggiungi Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TeamTrack API", Version = "v1" });
});
builder.Services.AddScoped<RepositoryUtente>();
builder.Services.AddScoped<ServiziUtente>();
builder.Services.AddScoped<RepositoryProgetto>();
builder.Services.AddScoped<ServiziTaskProgetto>();
builder.Services.AddScoped<RepositoryTaskProgetto>();
builder.Services.AddScoped<ServiziProgetto>();
builder.Services.AddScoped<GeneratoreCodiciAccesso>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ServiziQuickChart>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<TeamTrackDbContext>();
    try
    {
        dbContext.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Errore durante la migrazione: {ex.Message}");
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamTrack API v1");
        c.RoutePrefix = "swagger"; 
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Utente}/{action=Login}/{id?}");

app.Run();
