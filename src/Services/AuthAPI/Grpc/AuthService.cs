using Grpc.Core;
using GrpcAuthApi;
using MapsterMapper;
using MediatR;
using AuthAPI.Application.Queries;
using AuthAPI.Application.Commands;

namespace AuthAPI.Grpc;

public class AuthService : AuthApi.AuthApiBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthService(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async override Task<SignInResponse> SignIn(
        SignInRequest request,
        ServerCallContext context)
    {
        var authQuery = _mapper.Map<AuthenticateQuery>(request);
        var user = await _mediator.Send(authQuery, context.CancellationToken);

        if (user is null)
        {
            context.Status = new Status(
                StatusCode.PermissionDenied,
                "Invalid email or password.");

            return new SignInResponse();
        }

        var newIdTokenQuery = new NewIdTokenQuery(user);
        var idToken = await _mediator.Send(newIdTokenQuery);

        var newRefreshToken = new CreateRefreshTokenCommand(user);
        var refreshToken = await _mediator.Send(
            newRefreshToken,
            context.CancellationToken);

        return _mapper.Map<SignInResponse>((idToken, refreshToken));
    }
}