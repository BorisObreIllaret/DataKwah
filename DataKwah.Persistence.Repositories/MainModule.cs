using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Persistence.Repositories
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApplicationCommands(this IServiceCollection services)
        {
            return services;
        }
    }
}
