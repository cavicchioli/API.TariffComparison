using TariffComparison.Domain.Models;

namespace TariffComparison.Application.Responses
{
    public class CalculatedCostResponse
    {
        public TariffResponse Tariff { get; set; }

        public decimal Cost { get; set; }

        public string BillingType { get; set; }
    }
}
