using Bismuth.Crypto.Abstractions;
using Bismuth.Domain.Entities;
using Grpc.Core;
using GrpcUserApi;
using MapsterMapper;
using UserAPI.Abstractions;

namespace UserAPI.Grpc;

public class UserManager : UserApi.UserApiBase
{
    private readonly ILogger<UserManager> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public UserManager(
        ILogger<UserManager> logger,
        IUserRepository userRepository,
        IPasswordHashService passwordHashService,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }

    public override async Task<CreateUserResponse?> CreateUser(
        CreateUserRequest request,
        ServerCallContext context)
    {
        if (await _userRepository.GetUserByEmailAsync(request.Email, context.CancellationToken) is not null)
        {
            context.Status = new Status(StatusCode.AlreadyExists, $"User with email {request.Email} already exist.");
            return null;
        }

        var passwordHash = _passwordHashService.CreatePasswordHash(request.Password);

        var user = _mapper.Map<User>((request, passwordHash));

        await _userRepository.AddUserAsync(user, context.CancellationToken);

        // TODO: send user creation event to queue
        _logger.LogInformation("User with email {email} successfully created.", user.Email);

        return _mapper.Map<CreateUserResponse>(user);
    }

    public override async Task<GetUserResponse> GetUserByEmail(
        GetUserByEmailRequest request,
        ServerCallContext context)
    {
        if (await _userRepository.GetUserByEmailAsync(request.Email, context.CancellationToken) is not User user)
        {
            context.Status = new Status(StatusCode.NotFound, $"User with email {request.Email} not found.");
            return new GetUserResponse();
        }

        return _mapper.Map<GetUserResponse>(user);
    }

    public override async Task<GetUserResponse> GetUserById(
        GetUserByIdRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Id, out var id))
        {
            context.Status = new Status(StatusCode.InvalidArgument, "Invalid User ID provided.");
            return new GetUserResponse();
        }

        if (await _userRepository.GetUserByIdAsync(id, context.CancellationToken) is not User user)
        {
            context.Status = new Status(StatusCode.NotFound, $"User with ID {id} not found.");
            return new GetUserResponse();
        }

        return _mapper.Map<GetUserResponse>(user);
    }
}