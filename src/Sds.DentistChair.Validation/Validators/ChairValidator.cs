using FluentValidation;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;

namespace Sds.DentistChair.Validation.Validators;

public class ChairValidator : AbstractValidator<ChairDto>
{
    public ChairValidator()
    {
        RuleFor(chair => chair.Number)
            .NotEmpty().WithMessage("The chair number is required.")
            .MaximumLength(10).WithMessage("The chair number must not exceed 10 characters.");

        RuleFor(chair => chair.Description)
            .NotEmpty().WithMessage("The description is required.")
            .MaximumLength(100).WithMessage("The description must not exceed 100 characters.");

        RuleFor(chair => chair.AdditionalInfo)
            .MaximumLength(200).WithMessage("The additional information must not exceed 200 characters.");
    }
}
