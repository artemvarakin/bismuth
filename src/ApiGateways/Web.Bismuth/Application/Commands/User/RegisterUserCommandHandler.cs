using GrpcUserApi;
using MapsterMapper;
using MediatR;
using Web.Bismuth.Application.Common.User;
using static GrpcUserApi.UserApi;

namespace Web.Bismuth.Application.Commands.User;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly UserApiClient _userApiClient;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
        UserApiClient userApiClient,
        IMapper mapper)
    {
        _userApiClient = userApiClient;
        _mapper = mapper;
    }

    public async Task<RegisterUserResult> Handle(
        RegisterUserCommand command,
        CancellationToken token)
    {
        var grpcRequest = _mapper.Map<CreateUserRequest>(command);
        var response = await _userApiClient
            .CreateUserAsync(grpcRequest, cancellationToken: token);

        return _mapper.Map<RegisterUserResult>(response);
    }
}