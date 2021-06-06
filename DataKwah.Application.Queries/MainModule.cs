using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Application.Queries
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApplicationQueries(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MainModule).Assembly);
            services.AddAutoMapper(typeof(MainModule).Assembly);
            return services;
        }
    }
}
