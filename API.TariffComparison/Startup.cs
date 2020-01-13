using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using TariffComparison.Infra.CrossCutting.IoC;

namespace API.TariffComparison
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("TariffComparison.Application");
            services.AddMvc();
            services.AddBootStrapperIoC();
            services.AddMediatR(assembly);
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API - Electricity Tariffs Comparison",
                    Description = "Compare electricity tariffs based in your consume and find the cheapest.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Victor Cavichiolli",
                        Email = "xvictorprado@gmail.com",
                        Url = new Uri("https://github.com/cavicchioli")
                    },
                });

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API - Electricity Tariff Comparison v1");
            });
        }
    }
}
