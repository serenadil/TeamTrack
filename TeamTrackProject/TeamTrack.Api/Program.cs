using Microsoft.EntityFrameworkCore;
using TeamTrack.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Aggiungi il `DbContext` di `TeamTrack.Infrastructure`
builder.Services.AddDbContext<TeamTrackDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
