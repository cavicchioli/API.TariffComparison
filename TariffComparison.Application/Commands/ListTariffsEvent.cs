using MediatR;
using System.Collections.Generic;
using TariffComparison.Application.Responses;

namespace TariffComparison.Application.Commands
{
    public class ListTariffsEvent : IRequest<IEnumerable<TariffResponse>>
    {
    }
}
