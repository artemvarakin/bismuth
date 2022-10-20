using Bismuth.Domain.Entities;
using MediatR;
using UserAPI.Abstractions;

namespace UserAPI.Application.Queries;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByEmailAsync(query.Email, cancellationToken);
    }
}