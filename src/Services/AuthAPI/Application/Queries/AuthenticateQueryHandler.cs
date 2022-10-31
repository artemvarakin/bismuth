using Bismuth.Crypto.Abstractions;
using Bismuth.Domain.Entities;
using MediatR;
using AuthAPI.Abstractions;

namespace AuthAPI.Application.Queries;

public class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, User?>
{
    private readonly ILogger<AuthenticateQueryHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public AuthenticateQueryHandler(
        ILogger<AuthenticateQueryHandler> logger,
        IUserRepository userRepository,
        IPasswordHashService passwordHashService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }

    public async Task<User?> Handle(
        AuthenticateQuery command,
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