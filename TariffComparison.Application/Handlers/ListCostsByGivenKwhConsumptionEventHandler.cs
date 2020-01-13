using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TariffComparison.Application.Commands;
using TariffComparison.Application.Responses;
using TariffComparison.Domain.Interfaces;
using TariffComparison.Domain.Models.ValueObjects;

namespace TariffComparison.Application.Handlers
{
    public class ListCostsByGivenKwhConsumptionEventHandler : IRequestHandler<ListCostsByGivenKwhConsumptionEvent, IEnumerable<CalculatedCostResponse>>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IMapper _mapper;

        public ListCostsByGivenKwhConsumptionEventHandler(ITariffRepository tariffRepository, IMapper mapper)
        {
            _tariffRepository = tariffRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CalculatedCostResponse>> Handle(ListCostsByGivenKwhConsumptionEvent request, CancellationToken cancellationToken)
        {
            var calculedCosts = new List<CalculatedCostResponse>();

            var tariffs = await _tariffRepository.GetAllTariffs();
            foreach (var tariff in tariffs)
            {
                switch (tariff.PlanType)
                {
                    case PlanType.Monthly:

                        if (request.AnnualKwhConsumption > tariff.KilowattHourAllowance * 12)
                        {
                            calculedCosts.Add(new CalculatedCostResponse
                            {
                                Tariff = _mapper.Map<TariffResponse>(tariff),
                                BillingType = Enum.GetName(typeof(PlanType), PlanType.Annual),
                                Cost = tariff.Cost * 12
                                        + (request.AnnualKwhConsumption - tariff.KilowattHourAllowance * 12) * tariff.KilowattHourCost
                            });
                        }
                        else
                        {
                            calculedCosts.Add(new CalculatedCostResponse
                            {
                                Tariff = _mapper.Map<TariffResponse>(tariff),
                                BillingType = Enum.GetName(typeof(PlanType), PlanType.Annual),
                                Cost = tariff.Cost * 12
                            });
                        }

                        break;
                    default:

                        if (request.AnnualKwhConsumption > tariff.KilowattHourAllowance)
                        {
                            calculedCosts.Add(new CalculatedCostResponse
                            {
                                Tariff = _mapper.Map<TariffResponse>(tariff),
                                BillingType = Enum.GetName(typeof(PlanType), PlanType.Annual),
                                Cost = tariff.Cost
                                        + (request.AnnualKwhConsumption - tariff.KilowattHourAllowance) * tariff.KilowattHourCost
                            });
                        }
                        else
                        {
                            calculedCosts.Add(new CalculatedCostResponse
                            {
                                Tariff = _mapper.Map<TariffResponse>(tariff),
                                BillingType = Enum.GetName(typeof(PlanType), PlanType.Annual),
                                Cost = tariff.Cost
                            });
                        }

                        break;
                }
            }

            return _mapper.Map<IEnumerable<CalculatedCostResponse>>(calculedCosts.OrderBy(s=>s.Cost));
        }
    }
}
