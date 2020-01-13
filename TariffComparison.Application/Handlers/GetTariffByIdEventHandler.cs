using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Responses;
using TariffComparison.Domain.Interfaces;

namespace TariffComparison.Application.Handlers
{
    public class GetTariffByIdEventHandler : IRequestHandler<GetTariffByIdEvent, TariffResponse>
    {
      
        private readonly ITariffRepository _tariffRepository;
        private readonly IMapper _mapper;
        public GetTariffByIdEventHandler(ITariffRepository tariffRepository, IMapper mapper)
        {
            _tariffRepository = tariffRepository;
            _mapper = mapper;
        }

        public async Task<TariffResponse> Handle(GetTariffByIdEvent request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TariffResponse>(await _tariffRepository.GetTariffById(request.Id));
        }
    }
}
