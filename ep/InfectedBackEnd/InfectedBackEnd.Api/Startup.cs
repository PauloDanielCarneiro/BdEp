using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases;
using InfectedBackEnd.Application.UseCases.Diseases;
using InfectedBackEnd.Application.UseCases.Locations;
using InfectedBackEnd.Application.UseCases.User;
using InfectedBackEnd.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyApp.WebAPI.Documentation;

namespace InfectedBackEnd.Api
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
            InjectDependencies(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "InfectedBackEnd.Api", Version = "v1"});
                c.OperationFilter<FromQueryModelFilter>();
            });
        }

        private static void InjectDependencies(IServiceCollection services)
        {
            services.AddSingleton<IAggregationRepository, AggregationRepository>();
            services.AddSingleton<IUserLocationRepository, UserLocationRepository>();
            services.AddSingleton<IUserDiseaseRepository, UserDiseaseRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddTransient<IUserUseCase, UserUseCase>();

            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddTransient<ILocationsUseCase, LocationsUseCase>();

            services.AddSingleton<IDiseasesRepository, DiseasesRepository>();
            services.AddTransient<IDiseasesUseCase, DiseasesUseCase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfectedBackEnd.Api v1"));
            

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
