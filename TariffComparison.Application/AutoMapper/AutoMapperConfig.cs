﻿using AutoMapper;

namespace TariffComparison.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToResponseProfile());

            });
        }
    }
}