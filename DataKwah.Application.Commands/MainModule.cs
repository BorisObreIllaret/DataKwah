using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Application.Commands
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApplicationCommands(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MainModule).Assembly);
            services.AddAutoMapper(typeof(MainModule).Assembly);
            return services;
        }
    }
}
