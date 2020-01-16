using FluentValidation;
using TariffComparison.Application.Commands;

namespace TariffComparison.Application.Validations
{
    public class GetTariffByIdEventValidation : AbstractValidator<GetTariffByIdEvent>
    {
        public GetTariffByIdEventValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tariff Id is Required.");


            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Tariff Id Must Be Greater Than Zero.");
        }
    }
}
