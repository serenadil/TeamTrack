using Microsoft.EntityFrameworkCore;
using TeamTrack.Infrastructure;
using TeamTrack.Application.Servicies;
using TeamTrack.Application.Repositories; // Assicurati di importare il namespace corretto

var builder = WebApplication.CreateBuilder(args);

// Aggiungi il contesto del database
builder.Services.AddTeamTrackDbContext(builder.Configuration);

// Registrare RepositoryUtente
builder.Services.AddScoped<RepositoryUtente>();

// Registrare il servizio ServiziUtente
builder.Services.AddScoped<ServiziUtente>();

// Aggiungi i servizi MVC
builder.Services.AddControllers();

// Se utilizzi Swagger per la documentazione delle API, aggiungi questa configurazione
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa l'autenticazione e l'autorizzazione
app.UseAuthentication();
app.UseAuthorization();

// Mappa i controller
app.MapControllers();

app.Run();
