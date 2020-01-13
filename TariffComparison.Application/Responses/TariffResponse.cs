using System;

namespace TariffComparison.Application.Responses
{
    public class TariffResponse
    {
        public int Id { get; set; }
        public DateTime InclusionDate { get; set; }
        public DateTime ExclusionDate { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int KilowattHourAllowance { get; set; }
        public decimal KilowattHourCost { get; set; }
        public string PlanType { get; set; }
    }
}
