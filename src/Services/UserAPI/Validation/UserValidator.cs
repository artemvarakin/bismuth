using Bismuth.Domain.Entities;
using FluentValidation;

namespace UserAPI.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Id).Null();
        RuleFor(u => u.FirstName).Length(2, 20);
        RuleFor(u => u.LastName).Length(2, 20);
        RuleFor(u => u.Email).EmailAddress();
        RuleFor(u => u.PasswordHash).NotEmpty();
    }
}