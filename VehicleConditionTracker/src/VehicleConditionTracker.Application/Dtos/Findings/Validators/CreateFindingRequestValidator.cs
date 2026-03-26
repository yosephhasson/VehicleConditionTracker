using FluentValidation;

namespace VehicleConditionTracker.Application.Dtos.Findings.Validators;

public class CreateFindingRequestValidator : AbstractValidator<CreateFindingRequest>
{
    public CreateFindingRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2048);
    }
}
