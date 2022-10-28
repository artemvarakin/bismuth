using Grpc.Core;
using GrpcSessionApi;
using MapsterMapper;
using MediatR;
using SessionAPI.Abstractions;
using SessionAPI.Application.Commands;

namespace SessionAPI.Grpc;

public class SessionService : SessionApi.SessionApiBase
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