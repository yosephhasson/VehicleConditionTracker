using FluentValidation;

namespace VehicleConditionTracker.Application.Dtos.Reports.Validators;

public class UpdateVehicleReportRequestValidator : AbstractValidator<UpdateVehicleReportRequest>
{
    public UpdateVehicleReportRequestValidator()
    {
        RuleFor(x => x.Vin).NotEmpty().MaximumLength(32).MinimumLength(5);
        RuleFor(x => x.Year).InclusiveBetween(1886, DateTime.UtcNow.Year + 1);
        RuleFor(x => x.Make).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Model).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Color).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.InspectorNotes).MaximumLength(4000).When(x => x.InspectorNotes != null);
    }
}
