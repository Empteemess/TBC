using Application.Dtos.PhoneInfo;
using FluentValidation;

namespace Application.Validators;

public class PhoneInfoValidator : AbstractValidator<PhoneInfoDto>
{
    public PhoneInfoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .Length(4, 50)
            .WithMessage("Number must be between 4 and 50 characters.");
    }
}