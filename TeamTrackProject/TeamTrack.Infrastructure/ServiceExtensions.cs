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
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTeamTrackDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamTrackDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
