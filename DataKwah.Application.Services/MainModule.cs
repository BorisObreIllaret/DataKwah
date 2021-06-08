using DataKwah.Application.Services.Product;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Application.Services
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIndexationService, IndexationService>();
            return services;
        }
    }
}