using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TariffComparison.Application.AutoMapper;
using TariffComparison.Domain.Interfaces;
using TariffComparison.Infra.Data.Repository;

namespace TariffComparison.Infra.CrossCutting.IoC
{
    public static class BootStrapper
    {
        public static IServiceCollection AddBootStrapperIoC(
            this IServiceCollection services
        )
        {
            services.AddScoped<IMapper>(sp => new Mapper(AutoMapperConfig.RegisterMappings()))

            //Repositories
            .AddSingleton<ITariffRepository, TariffRepository>();

            return services;
        }
    }
}
