using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TariffComparison.Application.AutoMapper;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Notification;
using TariffComparison.Application.Validations;
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
            .AddSingleton<ITariffRepository, TariffRepository>()

            //Validators
            .AddTransient<IValidator<GetTariffByIdEvent>, GetTariffByIdEventValidation>()
            .AddTransient<IValidator<ListCostsByGivenKwhConsumptionEvent>, ListCostsByGivenKwhConsumptionEventValidation>()

            //Validators Notification
            .AddScoped<INotificationContext, NotificationContext>();

            return services;
        }
    }
}
