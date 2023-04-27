using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Application.Interfaces;

namespace Shortener.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<UrlDbContext>(option =>
            {
                option.UseSqlServer(connectionString);
                //option.UseSqlite(connectionString);
            });

            services.AddScoped<IUrlDbContext>(provider => provider
                .GetService<UrlDbContext>());

            return services;
        }
    }
}
