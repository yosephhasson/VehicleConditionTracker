using FluentValidation;

namespace VehicleConditionTracker.Application.Dtos.Findings.Validators;

public class UpdateFindingRequestValidator : AbstractValidator<UpdateFindingRequest>
{
    public UpdateFindingRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2048);
    }
}
