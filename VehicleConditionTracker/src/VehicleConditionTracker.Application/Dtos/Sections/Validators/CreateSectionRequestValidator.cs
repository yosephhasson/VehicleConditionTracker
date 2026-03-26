using FluentValidation;

namespace VehicleConditionTracker.Application.Dtos.Sections.Validators;

public class CreateSectionRequestValidator : AbstractValidator<CreateSectionRequest>
{
    public CreateSectionRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(128);
        RuleFor(x => x.SortOrder).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Notes).MaximumLength(2000).When(x => x.Notes != null);
    }
}
