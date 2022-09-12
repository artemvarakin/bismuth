using Bismuth.Domain.Entities;
using BismuthAPI.Abstractions.Repositories;
using BismuthAPI.Abstractions.Services;
using BismuthAPI.Contracts.User;
using MapsterMapper;

namespace BismuthAPI.Services;

public sealed class UserManagerService : IUserManagerService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public UserManagerService(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService,
        IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHashService = passwordHashService ?? throw new ArgumentNullException(nameof(passwordHashService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc />
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken token)
        => await _userRepository.GetUserByEmailAsync(email, token);

    /// <inheritdoc />
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, CancellationToken token)
    {
        var userId = Guid.NewGuid();
        var passwordHash = _passwordHashService.CreatePasswordHash(request.Password);
        var user = _mapper.Map<User>((request, userId, passwordHash));

        await _userRepository.AddUserAsync(user, token);

        var createdUser = await _userRepository.GetUserByEmailAsync(user.Email, token);
        if (createdUser is null) throw new InvalidOperationException("Could not register user.");

        return _mapper.Map<RegisterUserResponse>(createdUser);
    }
}