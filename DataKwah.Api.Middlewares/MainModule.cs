using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DataKwah.Api.Middlewares
{
    public static class MainModule
    {
        public static IServiceCollection ConfigureApi(this IServiceCollection services)
        {
            services
                .AddControllers(options => { options.Filters.Add(new ApplicationExceptionFilterAttribute()); })
                .AddNewtonsoftJson();

            services.PostConfigure<MvcNewtonsoftJsonOptions>(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

            return services;
        }
    }
}
