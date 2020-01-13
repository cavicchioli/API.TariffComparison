using Newtonsoft.Json;
using System;
using TariffComparison.Domain.Models.ValueObjects;

namespace TariffComparison.Domain.Models
{
    public class Tariff
    {
        public int Id { get; set; }
        public DateTime InclusionDate { get; set; }
        public DateTime ExclusionDate { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int KilowattHourAllowance { get; set; }
        public decimal KilowattHourCost { get; set; }
        public PlanType PlanType { get; set; }
    }
}
