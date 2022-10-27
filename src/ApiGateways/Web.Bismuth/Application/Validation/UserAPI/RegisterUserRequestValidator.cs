using Bismuth.Contracts.v1.User;
using FluentValidation;

namespace Web.Bismuth.Application.Validation.UserAPI;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(r => r.FirstName)
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(r => r.LastName)
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(r => r.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(r => r.Password)
            .NotNull()
            .MinimumLength(8)
            .MaximumLength(16);
    }
}