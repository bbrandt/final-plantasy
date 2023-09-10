using FluentValidation;
using TRS.FinalPlantasy.Application.Abstractions.Planning;

namespace TRS.FinalPlantasy.Application.Planning;

internal class PlanEntryModelValidator : AbstractValidator<PlanEntryModel>
{
    public PlanEntryModelValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.EventDate)
            .NotEmpty();

        RuleFor(x => x.PlanType)
            .NotEmpty();

        RuleFor(x => x.RepeatOn)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}
