using GrpcAuthApi;
using MapsterMapper;
using MediatR;
using Web.Bismuth.Application.Common.Auth;
using static GrpcAuthApi.AuthApi;

namespace Web.Bismuth.Application.Commands.Auth;

public class SignInCommandHadler : IRequestHandler<SignInCommand, SignInResult>
{
    private readonly AuthApiClient _authApiClient;
    private readonly IMapper _mapper;

    public SignInCommandHadler(
        AuthApiClient authApiClient,
        IMapper mapper)
    {
        _authApiClient = authApiClient;
        _mapper = mapper;
    }

    public async Task<SignInResult> Handle(
        SignInCommand command,
        CancellationToken cancellationToken)
    {
        var grpcRequest = _mapper.Map<SignInRequest>(command);
        var response = await _authApiClient.SignInAsync(
            grpcRequest,
            cancellationToken: cancellationToken);

        return _mapper.Map<SignInResult>(response);
    }
}