using Grpc.Core;
using GrpcAuthApi;
using MapsterMapper;
using MediatR;
using AuthAPI.Abstractions;
using AuthAPI.Application.Commands;

namespace AuthAPI.Grpc;

public class SessionService : AuthApi.AuthApiBase
{
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public SessionService(
        IMediator mediator,
        IJwtService jwtService,
        IMapper mapper)
    {
        _mediator = mediator;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async override Task<SignInResponse> SignIn(
        SignInRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<SignInCommand>(request);
        var user = await _mediator.Send(command, context.CancellationToken);

        if (user is null)
        {
            context.Status = new Status(StatusCode.PermissionDenied, "Invalid email or password.");
            return new SignInResponse();
        }

        var (idToken, refreshToken) = await _jwtService.CreateNewTokenPairAsync(
            user,
            context.CancellationToken);

        return _mapper.Map<SignInResponse>((idToken, refreshToken));
    }
}