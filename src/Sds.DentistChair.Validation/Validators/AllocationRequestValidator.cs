using FluentValidation;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;

namespace Sds.DentistChair.Validation.Validators;

public class AllocationRequestValidator : AbstractValidator<AllocationRequest>
{
    public AllocationRequestValidator()
    {
        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("StartTime is required");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("EndTime is required");
    }
}
