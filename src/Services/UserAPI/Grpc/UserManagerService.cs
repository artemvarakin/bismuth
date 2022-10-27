using Bismuth.Crypto.Abstractions;
using Bismuth.Domain.Entities;
using Grpc.Core;
using GrpcUserApi;
using MapsterMapper;
using MediatR;
using UserAPI.Application.Queries;

namespace UserAPI.Grpc;

public class UserManagerService : UserApi.UserApiBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserManagerService> _logger;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public UserManagerService(
        IMediator mediator,
        ILogger<UserManagerService> logger,
        IPasswordHashService passwordHashService,
        IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }

    public override async Task<CreateUserResponse> CreateUser(
        CreateUserRequest request,
        ServerCallContext context)
    {
        var passwordHash = _passwordHashService.CreatePasswordHash(request.Password);

        var command = new CreateUserCommand(
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            PasswordHash: passwordHash
        );
        var user = await _mediator.Send(command, context.CancellationToken);

        // TODO: send user creation event to queue
        _logger.LogInformation("User with email {email} successfully created.", user.Email);

        return _mapper.Map<CreateUserResponse>(user);
    }

    public override async Task<GetUserResponse> GetUserByEmail(
        GetUserByEmailRequest request,
        ServerCallContext context)
    {
        var query = new GetUserByEmailQuery(request.Email);
        if (await _mediator.Send(query, context.CancellationToken) is not User user)
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

        var query = new GetUserByIdQuery(id);
        if (await _mediator.Send(query, context.CancellationToken) is not User user)
        {
            context.Status = new Status(StatusCode.NotFound, $"User with ID {id} not found.");
            return new GetUserResponse();
        }

        return _mapper.Map<GetUserResponse>(user);
    }
}