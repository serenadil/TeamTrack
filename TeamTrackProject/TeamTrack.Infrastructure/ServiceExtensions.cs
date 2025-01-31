using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace TeamTrack.Infrastructure
{
    /// <summary>
    /// Classe statica che fornisce metodi di estensione per configurare i servizi in un'applicazione ASP.NET Core.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Aggiunge e configura il contesto del database di TeamTrack al contenitore di dipendenze.
        /// </summary>
        /// <param name="services">La collezione di servizi dove aggiungere il contesto del database.</param>
        /// <param name="configuration">La configurazione per leggere le stringhe di connessione e altre impostazioni.</param>
        /// <returns>Restituisce la stessa collezione di servizi per permettere il chaining dei metodi.</returns>
        public static IServiceCollection AddTeamTrackDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura il contesto del database utilizzando una stringa di connessione da appsettings.json
            services.AddDbContext<TeamTrackDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Restituisce la collezione di servizi per consentire il chaining.
            return services;
        }
    }
}

