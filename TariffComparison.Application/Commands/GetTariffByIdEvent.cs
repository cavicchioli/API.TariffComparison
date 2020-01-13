using MediatR;
using TariffComparison.Application.Responses;

namespace TariffComparison.Application.Commands
{
    public class GetTariffByIdEvent : IRequest<TariffResponse>
    {
        public int Id { get; set; }
    }
}
