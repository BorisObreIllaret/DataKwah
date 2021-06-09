using DataKwah.Api.Middlewares;
using DataKwah.Application.Commands;
using DataKwah.Application.Queries;
using DataKwah.Application.Services;
using DataKwah.Persistence;
using DataKwah.Persistence.Contexts;
using DataKwah.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DataKwah.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataKwah.Api", Version = "v1" }); });
            services.ConfigureApi();
            services.ConfigureApplicationCommands();
            services.ConfigureApplicationQueries();
            services.ConfigureApplicationServices();
            services.ConfigurePersistenceDbContexts(Configuration.GetConnectionString("sqlite"));
            services.ConfigurePersistenceRepositories();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataKwahDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataKwah.Api v1"));
            }

            //dbContext.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
