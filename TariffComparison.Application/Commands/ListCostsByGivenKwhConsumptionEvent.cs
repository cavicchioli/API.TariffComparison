using MediatR;
using System.Collections.Generic;
using TariffComparison.Application.Responses;

namespace TariffComparison.Application.Commands
{
    public class ListCostsByGivenKwhConsumptionEvent : IRequest<IEnumerable<CalculatedCostResponse>>
    {
        public int AnnualKwhConsumption { get; set; }
    }
}
