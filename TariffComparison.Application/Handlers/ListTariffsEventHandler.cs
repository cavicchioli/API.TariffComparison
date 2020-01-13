using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Responses;
using TariffComparison.Domain.Interfaces;

namespace TariffComparison.Application.Handlers
{
    public class ListTariffsEventHandler : IRequestHandler<ListTariffsEvent, IEnumerable<TariffResponse>>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IMapper _mapper;
        public ListTariffsEventHandler(ITariffRepository tariffRepository, IMapper mapper)
        {
            _tariffRepository = tariffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TariffResponse>> Handle(ListTariffsEvent request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<TariffResponse>>(await _tariffRepository.GetAllTariffs());
        }
    }
}
