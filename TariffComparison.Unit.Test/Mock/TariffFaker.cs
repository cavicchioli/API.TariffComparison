using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TariffComparison.Domain.Models;
using TariffComparison.Domain.Models.ValueObjects;

namespace TariffComparison.Unit.Test.Mock
{
    public class TariffFaker
    {
        //using bogus to create a mocked data base
        private int numberOfTariffs;
        private int tariffIds;
        private Faker<Tariff> tariffFaker;
        public TariffFaker()
        {
            numberOfTariffs = new Random().Next(1, 100);
            tariffFaker = GetFakerInstance();
            tariffIds = 1;
        }

        private Faker<Tariff> GetFakerInstance()
        {
            return new Faker<Tariff>()
                .RuleFor(s => s.Id, f => tariffIds++)
                .RuleFor(s => s.Name, f => $"{f.Lorem.Word()} tariff")
                .RuleFor(s => s.InclusionDate, f => f.Date.Past())
                .RuleFor(s => s.KilowattHourAllowance, f => f.Random.Int(0, 5000) * f.Random.Int(0, 1))
                .RuleFor(s => s.KilowattHourCost, f => Math.Round(f.Random.Decimal(1, 100) / 100, 2))
                .RuleFor(s => s.PlanType, f => f.PickRandom<PlanType>())
                .RuleFor(s => s.Cost, f => Math.Round(f.Random.Decimal(5, 1000), 2));
        }

        public async Task<IEnumerable<Tariff>> GetAllTariffsMock()
        {
            return await Task.FromResult<IEnumerable<Tariff>>(tariffFaker.Generate(numberOfTariffs));
        }

        public async Task<Tariff> GetTariffByIdMock(int id)
        {
            var tariff = tariffFaker.Generate();
            tariff.Id = id;

            return await Task.FromResult(tariff);
        }
    }
}
