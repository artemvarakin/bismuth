using Bismuth.Domain.Entities;
using MediatR;
using UserAPI.Abstractions;

namespace UserAPI.Application.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        => await _userRepository.GetUserByIdAsync(query.Id, cancellationToken);
}