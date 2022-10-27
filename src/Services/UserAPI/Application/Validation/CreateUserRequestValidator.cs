using FluentValidation;
using GrpcUserApi;
using UserAPI.Abstractions;

namespace UserAPI.Application.Validation;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    private const string _emailRule = @"^[^@\s]+@[^@\s]+\.[\w-]{2,4}$";
    private const string _specialCharacterRule = @"[\!@#$%^&*()\\[\]{}\-_+=~`|:;""'<>,./?]";
    private const string _digitRule = "[0-9]";
    private const string _lowerCaseRule = "[a-z]";
    private const string _upperCaseRule = "[A-Z]";

    private readonly IUserRepository _userRepository;

    public CreateUserRequestValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(r => r.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(r => r.Email)
            .Matches(_emailRule)
            .WithMessage("{PropertyName} is in incorrect format.")
            .MustAsync(async (email, cancellation) =>
                !await _userRepository.IsUserExists(email, cancellation))
            .WithMessage("User already exists.");

        RuleFor(r => r.Password)
            .NotEmpty()
            .Matches(_digitRule)
            .WithMessage("{PropertyName} should contain at least one digit.")
            .Matches(_specialCharacterRule)
            .WithMessage("{PropertyName} should contain at least one special character.")
            .Matches(_lowerCaseRule)
            .WithMessage("Pas{PropertyName} should contain at least one lowercase character.")
            .Matches(_upperCaseRule)
            .WithMessage("{PropertyName} should contain at least one uppercase character.")
            .MinimumLength(8)
            .MaximumLength(16);
    }
}