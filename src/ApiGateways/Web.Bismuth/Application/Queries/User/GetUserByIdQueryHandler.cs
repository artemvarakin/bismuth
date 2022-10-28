using GrpcUserApi;
using MapsterMapper;
using MediatR;
using Web.Bismuth.Application.Common.User;

namespace Web.Bismuth.Application.Queries.User;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserResult?>
{
    private readonly UserApi.UserApiClient _userApiClient;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        UserApi.UserApiClient userApiClient,
        IMapper mapper)
    {
        _userApiClient = userApiClient;
        _mapper = mapper;
    }

    public async Task<GetUserResult?> Handle(
        GetUserByIdQuery query,
        CancellationToken token)
    {
        var grpcRequest = new GetUserByIdRequest { Id = query.Id.ToString() };
        var response = await _userApiClient
            .GetUserByIdAsync(grpcRequest, cancellationToken: token);

        return response is null ? null : _mapper.Map<GetUserResult>(response);
    }
}