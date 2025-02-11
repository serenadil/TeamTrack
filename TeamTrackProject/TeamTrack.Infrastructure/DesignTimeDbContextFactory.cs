using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTrack.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TeamTrackDbContext>
    {
        public TeamTrackDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TeamTrackDbContext>();

            // Carica la configurazione dal file appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configura il provider SQL Server usando la stringa di connessione
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new TeamTrackDbContext(optionsBuilder.Options);
        }
    }
}