using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TeamTrack.Infrastrutture;
using TeamTrack.Servizi;
using TeamTrack.Servizi.Repository;
using TeamTrack.Servizi.Servizi;



var builder = WebApplication.CreateBuilder(args);

// ?? Caricamento esplicito della configurazione
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// ?? Recupero della stringa di connessione e debug
var connectionString = config.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection String: {connectionString}");  // Controlla se viene stampata correttamente

// ?? Registrazione del DbContext con la stringa di connessione
builder.Services.AddDbContext<TeamTrackDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// ?? Registrazione dei servizi e repository
builder.Services.AddScoped<RepositoryUtente>();
builder.Services.AddScoped<ServiziUtente>();
builder.Services.AddScoped<RepositoryProgetto>();
builder.Services.AddScoped<ServiziTaskProgetto>();
builder.Services.AddScoped<RepositoryTaskProgetto>();
builder.Services.AddScoped<ServiziProgetto>();
builder.Services.AddScoped<GeneratoreCodiciAccesso>();



// ?? Aggiunta dei controller
builder.Services.AddControllers();

// ?? Configurazione Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeamTrack API",
        Version = "v1",
        Description = ""
    });
});

var app = builder.Build();

// ?? Middleware Swagger (solo in sviluppo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamTrack API V1");
        options.RoutePrefix = string.Empty;  // Swagger disponibile alla root dell'app
    });
}

// ?? Middleware aggiuntivi
app.UseHttpsRedirection();
app.UseAuthorization();

// ?? Mappa i controller
app.MapControllers();

// ?? Avvio dell'applicazione
app.Run();
