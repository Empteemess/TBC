using Application.Dtos.ApplicationUser;
using FluentValidation;

namespace Application.Validators;

public class ApplicationUserDtoValidator : AbstractValidator<ApplicationUserDto>
{
    public ApplicationUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .Matches(@"^(?:[a-zA-Z]{2,50}|\u10A0-\u10FF]{2,50})$")
            .WithMessage("The first name must be at least 2 and at most 50 characters long," +
                         "It must contain only Georgian or Latin alphabet letters," +
                         "It must not contain both Latin and Georgian letters at the same time.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .Matches(@"^(?:[a-zA-Z]{2,50}|\u10A0-\u10FF]{2,50})$")
            .WithMessage("The last name must be at least 2 and at most 50 characters long," +
                         "It must contain only Georgian or Latin alphabet letters," +
                         "It must not contain both Latin and Georgian letters at the same time.")
            .MaximumLength(50);

        RuleFor(x => x.PersonalId)
            .Length(11, 11)
            .WithMessage("PersonId must be exact 11 characters length.");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18))
            .WithMessage("You must be at least 18 years old.");
    }
}
