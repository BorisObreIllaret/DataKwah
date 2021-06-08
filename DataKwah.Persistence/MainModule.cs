using DataKwah.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Persistence
{
    public static class MainModule
    {
        /// <summary>
        ///     Configure Entity Framework Core DbContexts.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="connectionString">Database connexion string.</param>
        /// <returns>Services.</returns>
        public static IServiceCollection ConfigurePersistenceDbContexts(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataKwahDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite(connectionString, builder => builder.MigrationsAssembly("DataKwah.Persistence.Migrations"));
            });

            return services;
        }
    }
}