using AutoMapper;
using System;
using TariffComparison.Application.Responses;
using TariffComparison.Domain.Models;
using TariffComparison.Domain.Models.ValueObjects;

namespace TariffComparison.Application.AutoMapper
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {

            CreateMap<Tariff, TariffResponse>()
                .ForMember(dest => dest.PlanType, src => src.MapFrom(m => MapPlanType(m)));

        }

        private string MapPlanType(Tariff src)
        {
            if (Enum.IsDefined(typeof(PlanType), src.PlanType))
            {
                return Enum.GetName(typeof(PlanType), src.PlanType);
            }

            return default;
        }
    }
}
