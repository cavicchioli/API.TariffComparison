using AutoMapper;
using Moq;
using System;
using System.Linq;
using System.Threading;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Handlers;
using TariffComparison.Domain.Interfaces;
using TariffComparison.Unit.Test.Helper;
using TariffComparison.Unit.Test.Mock;
using Xunit;

namespace TariffComparison.Unit.Test
{
    public class TariffComparisonTests
    {
        [Fact(DisplayName = "Calculate Final Cost Using A List Of Tariffs")]
        [Trait("TariffComparison", "Handler: ListCostsByGivenKwhConsumptionEventHandler")]
        public async void ListCostsByGivenKwhConsumptionEventHandler_CalculateFinalCost_MustReturnNotNull()
        {
            var tariffFaker = new TariffFaker();
            var tariffRepository = new Mock<ITariffRepository>();
      
            var randomAnnualKwhConsumption = new Random().Next(1500, 10000);
            tariffRepository.Setup(r => r.GetAllTariffs())
                .Returns(tariffFaker.GetAllTariffsMock());

            var consumptionEventHandler = new ListCostsByGivenKwhConsumptionEventHandler(tariffRepository.Object, AutoMapperSingleton.Mapper);

            // Act
            var finalCosts = await consumptionEventHandler.Handle(
                new ListCostsByGivenKwhConsumptionEvent() { 
                        AnnualKwhConsumption = randomAnnualKwhConsumption
                    }, new CancellationToken());

            // Assert
            Assert.NotNull(finalCosts);
            Assert.NotNull(finalCosts.FirstOrDefault().Tariff);
            Assert.NotEqual(0,finalCosts.FirstOrDefault().Cost,0);
            Assert.NotNull(finalCosts.FirstOrDefault().BillingType);
        }

        [Fact(DisplayName = "Return A List Of Tariffs")]
        [Trait("TariffComparison", "Handler: ListTariffsEventHandler")]
        public async void ListTariffsEventHandler_ReturnAllTariffs_MustReturnNotNull()
        {
            var tariffFaker = new TariffFaker();
            var tariffRepository = new Mock<ITariffRepository>();

            tariffRepository.Setup(r => r.GetAllTariffs())
                .Returns(tariffFaker.GetAllTariffsMock());

            var listTariffsEventHandler = new ListTariffsEventHandler(tariffRepository.Object, AutoMapperSingleton.Mapper);

            // Act
            var tariffs = await listTariffsEventHandler.Handle(
                new ListTariffsEvent(){
                    }, new CancellationToken());

            // Assert
            Assert.NotNull(tariffs);
        }

        [Fact(DisplayName = "Return A Tariff by Given Id")]
        [Trait("TariffComparison", "Handler: GetTariffByIdEventHandler")]
        public async void GetTariffByIdEventHandler_ReturnATariff_MustReturnNotNull()
        {
            var tariffFaker = new TariffFaker();
            var tariffs = tariffFaker.GetAllTariffsMock();
            var tariffId = tariffs.Result.FirstOrDefault().Id;
            var tariffRepository = new Mock<ITariffRepository>();

            tariffRepository.Setup(r => r.GetTariffById(tariffId))
                .Returns(tariffFaker.GetTariffByIdMock(tariffId));

            var getTariffByIdEventHandler = new GetTariffByIdEventHandler(tariffRepository.Object, AutoMapperSingleton.Mapper);

            // Act
            var tariff = await getTariffByIdEventHandler.Handle(
                new GetTariffByIdEvent()
                {
                    Id = tariffId
                }, new CancellationToken());

            // Assert
            Assert.NotNull(tariff);
        }
    }
}
