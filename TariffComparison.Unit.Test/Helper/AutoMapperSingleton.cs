using AutoMapper;
using TariffComparison.Application.AutoMapper;

namespace TariffComparison.Unit.Test.Helper
{
    public class AutoMapperSingleton
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    IMapper mapper = AutoMapperConfig.RegisterMappings().CreateMapper();
                    _mapper = mapper;
                }
                return _mapper;
            }
        }
    }
}
