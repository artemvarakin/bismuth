using Bismuth.Contracts.v1.User;
using GrpcUserApi;
using MapsterMapper;
using Web.Bismuth.Abstractions;
using GetUserResponse = Bismuth.Contracts.v1.User.GetUserResponse;

namespace Web.Bismuth.Services;

public class UserManager : IUserManager
{
    private readonly IMapper _mapper;
    private readonly UserApi.UserApiClient _userApiClient;

    public UserManager(
        IMapper mapper,
        UserApi.UserApiClient client)
    {
        _mapper = mapper;
        _userApiClient = client;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request, CancellationToken token)
    {
        var grpcRequest = _mapper.Map<CreateUserRequest>(request);
        var response = await _userApiClient.CreateUserAsync(grpcRequest, cancellationToken: token);

        return _mapper.Map<RegisterUserResponse>(response);
    }

    /// <inheritdoc />
    public async Task<GetUserResponse?> GetUserByEmailAsync(string email, CancellationToken token)
    {
        var response = await _userApiClient.GetUserByEmailAsync(new GetUserByEmailRequest { Email = email }, cancellationToken: token);
        if (response is null)
        {
            return null;
        }

        return _mapper.Map<GetUserResponse>(response);
    }
}