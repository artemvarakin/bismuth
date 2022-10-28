using Bismuth.Crypto.Abstractions;
using Bismuth.Domain.Entities;
using MediatR;
using AuthAPI.Abstractions;

namespace AuthAPI.Application.Commands;

public class SignInCommandHandler : IRequestHandler<SignInCommand, User?>
{
    private readonly ILogger<SignInCommandHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public SignInCommandHandler(
        ILogger<SignInCommandHandler> logger,
        IUserRepository userRepository,
        IPasswordHashService passwordHashService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    public async Task<User?> Handle(
        SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(
            command.Email,
            cancellationToken);

        if (user is null)
        {
            _logger.LogInformation(
                "Sign in attempt with unregistered email '{email}'.",
                command.Email);

            return null;
        }

        if (!_passwordHashService.IsPasswordValid(
            user.PasswordHash,
            command.Password))
        {
            _logger.LogInformation(
                "Sign in attempt with email '{email}' and invalid password.",
                command.Email);

            return null;
        }

        return user;
    }
}