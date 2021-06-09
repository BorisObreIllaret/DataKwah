using DataKwah.Application.Commands.Product.IndexOne;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DataKwah.Application.Commands
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApplicationCommands(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainModule).Assembly);
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IndexOneValidator>());
            services.AddMediatR(typeof(MainModule).Assembly);
            return services;
        }
    }
}
