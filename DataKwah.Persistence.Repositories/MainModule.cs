using DataKwah.Persistence.Repositories.Product;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Persistence.Repositories
{
    public static class MainModule
    {
        public static IServiceCollection ConfigurePersistenceRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}