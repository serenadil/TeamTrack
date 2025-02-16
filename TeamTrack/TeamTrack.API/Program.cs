using Microsoft.EntityFrameworkCore;
using TeamTrack.Infrastrutture;
using TeamTrack.Servizi.Repository;
using TeamTrack.Servizi.Servizi;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi la configurazione per il contesto DB e i servizi
builder.Services.AddScoped<RepositoryUtente>();  // Aggiungi questa riga per il repository
builder.Services.AddScoped<ServiziUtente>();    // Aggiungi questa riga per il servizio
builder.Services.AddDbContext<TeamTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Aggiungi i controller
builder.Services.AddControllers();

// Aggiungi il supporto per Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Aggiungi metadati per la documentazione di Swagger
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TeamTrack API",
        Version = "v1",
        Description = "Una breve descrizione della tua API"
    });
});

var app = builder.Build();

// Configura il middleware per Swagger solo in modalità sviluppo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamTrack API V1");
        options.RoutePrefix = string.Empty;  // Swagger UI sarà visibile alla root dell'app
    });
}

// Configura il resto del middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Mappa i controller
app.MapControllers();

// Esegui l'applicazione
app.Run();
