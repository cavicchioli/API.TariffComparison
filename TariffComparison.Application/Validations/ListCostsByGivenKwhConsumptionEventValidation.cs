using FluentValidation;
using TariffComparison.Application.Commands;

namespace TariffComparison.Application.Validations
{
    public class ListCostsByGivenKwhConsumptionEventValidation : AbstractValidator<ListCostsByGivenKwhConsumptionEvent>
    {
        public ListCostsByGivenKwhConsumptionEventValidation()
        {
            RuleFor(x => x.AnnualKwhConsumption)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Annual kWh Consumption Is Required.");

            RuleFor(x => x.AnnualKwhConsumption)
              .GreaterThan(0)
              .WithMessage("The Annual kWh Consumption Must Be Greater Than Zero.");
        }
    }
}
