using GrpcUserApi;
using MapsterMapper;
using MediatR;
using Web.Bismuth.Application.Common.User;

namespace Web.Bismuth.Application.Queries.User;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserResult?>
{
    private readonly UserApi.UserApiClient _userApiClient;
    private readonly IMapper _mapper;

    public GetUserByEmailQueryHandler(
        UserApi.UserApiClient userApiClient,
        IMapper mapper)
    {
        _userApiClient = userApiClient;
        _mapper = mapper;
    }

    public async Task<GetUserResult?> Handle(
        GetUserByEmailQuery query,
        CancellationToken token)
    {
        var grpcRequest = new GetUserByEmailRequest { Email = query.Email };
        var response = await _userApiClient
            .GetUserByEmailAsync(grpcRequest, cancellationToken: token);

        return response is null ? null : _mapper.Map<GetUserResult>(response);
    }
}